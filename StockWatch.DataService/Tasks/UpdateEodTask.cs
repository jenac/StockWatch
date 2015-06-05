using StockWatch.DataAccess.Repositories;
using StockWatch.Entities.Complex;
using StockWatch.Entities.Table;
using StockWatch.Internet;
using StockWatch.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
namespace StockWatch.DataService.Tasks
{
	public class UpdateEodTask : BaseUpdateTask
	{
		private readonly IDataRepository _dataRepo;
		public UpdateEodTask (IDataRepository dataRepo)
		{
			_dataRepo = dataRepo;
		}


		#region IServiceTask implementation
		public override void Execute ()
		{
			DateTime? lastTradeDate = InternetReader.GetLastTradeDate();
			if (!lastTradeDate.HasValue)
			{
				Logger.Instance.Error("Fail to get last trade date.");
				return;
			}
			else
			{
				Logger.Instance.InfoFormat("Last trade date is: {0}", lastTradeDate.Value.ToString("yyyy-MM-dd"));
			}

			List<DataState> states = _dataRepo.EodState;

			List<EodParam> parameters = states.Select(e => new EodParam
				{
					Symbol = e.Symbol,
					//For symbol without since, we need get 10 years of data
					Start = (e.Last.HasValue ? e.Last.Value : DateTime.Today.AddYears(-30)),
					End = lastTradeDate.Value
				}).Where(p => p.Start < lastTradeDate.Value).ToList();

			Logger.Instance.InfoFormat("{0} symbols to be processed", parameters.Count);
			int i = 0;
			const int batchSize = 16;
			while (true)
			{
				List<EodParam> processing = parameters.Skip(i * batchSize).Take(batchSize).ToList();
				if (processing.Count == 0)
					break;
				List<Eod> eods = ReadEods(processing);

				if (eods.Count > 0)
				{
					Logger.Instance.InfoFormat("Saving {0} eods ", eods.Count);
					UpdateHelper.BulkSave (_dataRepo, typeof(Eod),
						eods.Select (e => e.ToCsv ()).ToArray());
				}
				i++;
			}
		}
		#endregion

		private List<Eod> ReadEods(List<EodParam> parameters)
		{
			List<Eod> seed = new List<Eod> ();
			List<Eod> value =
				parameters.AsParallel()
					.Select(p => InternetReader.ReadEodBySymbol(p))
					.Aggregate(seed, (i, j) => i.Union(j).ToList());
			return value;
		}

	}
}

