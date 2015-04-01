using StockWatch.DataAccess.Repositories;
using StockWatch.Entities.Complex;
using StockWatch.Entities.Table;
using System;
using System.Collections.Generic;

namespace StockWatch.DataService.Test
{
	public class MocAnalyseRepository : IAnalyseRepository
	{
		public MocAnalyseRepository ()
		{
		}

		#region IAnalyseRepository implementation

		public List<DataState> LoadFullIndicatorStateByName (string name)
		{
			throw new NotImplementedException ();
		}

		public IEnumerable<ComputedEod> LoadComputedEod (string symbol = null)
		{
			return new List<ComputedEod> {
				new ComputedEod { Symbol="AAPL", Date=new DateTime(2012,02,21), GL=1.1400000000000006}, 
				new ComputedEod { Symbol="AAPL", Date=new DateTime(2012,02,22), GL=-0.009999999999990905}, 
				new ComputedEod { Symbol="AAPL", Date=new DateTime(2012,02,23), GL=0.18999999999999773}, 
				new ComputedEod { Symbol="AAPL", Date=new DateTime(2012,02,24), GL=0.39000000000000057}, 
				new ComputedEod { Symbol="AAPL", Date=new DateTime(2012,02,27), GL=0.6400000000000006}, 
				new ComputedEod { Symbol="AAPL", Date=new DateTime(2012,02,28), GL=1.0699999999999932}, 
				new ComputedEod { Symbol="AAPL", Date=new DateTime(2012,02,29), GL=0.11999999999999034}, 
				new ComputedEod { Symbol="AAPL", Date=new DateTime(2012,03,01), GL=-0.5300000000000011}, 
				new ComputedEod { Symbol="AAPL", Date=new DateTime(2012,03,02), GL=0.12999999999999545}, 
				new ComputedEod { Symbol="AAPL", Date=new DateTime(2012,03,05), GL=-1.75},
				new ComputedEod { Symbol="AAPL", Date=new DateTime(2012,03,06), GL=0.9399999999999977}, 
				new ComputedEod { Symbol="AAPL", Date=new DateTime(2012,03,07), GL=-0.8799999999999955}, 
				new ComputedEod { Symbol="AAPL", Date=new DateTime(2012,03,08), GL=1.0500000000000114}, 
				new ComputedEod { Symbol="AAPL", Date=new DateTime(2012,03,09), GL=0.14000000000000057}, 
				new ComputedEod { Symbol="AAPL", Date=new DateTime(2012,03,12), GL=0.4299999999999926}, 
				new ComputedEod { Symbol="AAPL", Date=new DateTime(2012,03,13), GL=1.509999999999991}, 
				new ComputedEod { Symbol="AAPL", Date=new DateTime(2012,03,14), GL=1.6500000000000057}, 
				new ComputedEod { Symbol="AAPL", Date=new DateTime(2012,03,15), GL=-2.009999999999991}, 
				new ComputedEod { Symbol="AAPL", Date=new DateTime(2012,03,16), GL=0.12000000000000455}, 
				new ComputedEod { Symbol="AAPL", Date=new DateTime(2012,03,19), GL=0.39000000000000057}, 
				new ComputedEod { Symbol="AAPL", Date=new DateTime(2012,03,20), GL=0.9299999999999926}, 
				new ComputedEod { Symbol="AAPL", Date=new DateTime(2012,03,21), GL=-0.04000000000000625}, 
				new ComputedEod { Symbol="AAPL", Date=new DateTime(2012,03,22), GL=0.21999999999999886}, 
				new ComputedEod { Symbol="AAPL", Date=new DateTime(2012,03,23), GL=-0.6299999999999955}, 
				new ComputedEod { Symbol="AAPL", Date=new DateTime(2012,03,26), GL=1.029999999999987}, 
				new ComputedEod { Symbol="AAPL", Date=new DateTime(2012,03,27), GL=1.1800000000000068}, 
				new ComputedEod { Symbol="AAPL", Date=new DateTime(2012,03,28), GL=-0.10999999999999943}, 
				new ComputedEod { Symbol="AAPL", Date=new DateTime(2012,03,29), GL=-0.4200000000000017}, 
				new ComputedEod { Symbol="AAPL", Date=new DateTime(2012,03,30), GL=-1.3199999999999932}, 
				new ComputedEod { Symbol="AAPL", Date=new DateTime(2012,04,02), GL=2.3999999999999915}, 
				new ComputedEod { Symbol="AAPL", Date=new DateTime(2012,04,03), GL=0.29000000000000625}, 
				new ComputedEod { Symbol="AAPL", Date=new DateTime(2012,04,04), GL=0}, 
			};

		}

		public void SaveIndicator (Indicator value)
		{
			throw new NotImplementedException ();
		}

		public IEnumerable<double> LoadClosePriceBySymbol (string symbol, bool sortAsc)
		{
			var closePrices = new List<double> {   
				129.5D, 128.45D, 128.72D, 127.83D, 127.08D, 126.46D, 124.88D, 122.02D, 119.72D, 118.93D, 119.94D, 
				119.56D, 118.65D, 118.63D, 117.16D, 118.9D, 115.31D, 109.14D, 113.1D, 112.98D, 112.4D, 109.55D, 
				108.72D, 105.99D, 106.82D, 109.8D, 110.22D, 109.25D, 112.01D, 111.89D, 107.75D, 106.26D, 106.25D,
				109.33D, 110.38D, 112.52D, 113.91D, 113.99D, 112.01D, 112.54D, 112.94D, 111.78D, 112.65D, 109.41D,
				106.74D, 108.22D, 109.73D, 111.62D, 111.95D, 114.12D, 112.4D, 115D, 115.49D, 115.93D, 114.63D,
				115.07D, 118.93D, 119D, 117.6D, 118.62D, 116.47D, 116.31D, 114.67D, 115.47D, 113.99D, 114.18D,
				112.82D, 111.25D, 109.7D, 108.83D, 109.01D, 108.7D, 108.86D, 108.6D, 109.4D, 108D, 106.98D,
				107.34D, 106.74D, 105.11D, 105.22D, 104.83D, 102.99D, 102.47D, 99.76D, 97.67D, 96.26D, 97.54D,
				98.75D, 99.81D, 100.73D, 101.02D, 100.8D, 98.75D, 99.62D, 99.62D, 99.9D, 99.18D, 100.75D,
				100.11D, 100.75D, 97.87D, 101.75D, 102.64D, 101.06D, 100.96D, 101.79D, 101.58D, 100.86D, 101.63D,
				101.66D, 101.43D, 101D, 97.99D, 98.36D, 98.97D, 98.12D, 98.94D, 103.3D, 102.5D, 102.25D,
				102.13D, 100.89D, 101.54D, 101.32D, 100.58D, 100.57D, 100.53D, 99.16D, 97.98D, 97.5D, 97.24D,
				95.97D, 95.99D, 94.74D, 94.48D, 94.96D, 95.12D, 95.59D, 96.13D, 95.6D, 98.15D, 98.38D,
				99.02D, 97.67D, 97.03D, 97.19D, 94.72D, 93.94D, 94.43D, 93.09D, 94.78D, 95.32D, 96.45D,
				95.22D, 95.04D, 95.39D, 95.35D, 95.97D, 94.03D, 93.48D, 93.52D, 92.93D, 91.98D, 90.9D,
				90.36D, 90.28D, 90.83D, 90.91D, 91.86D, 92.18D, 92.08D, 92.2D, 91.28D, 92.29D, 93.86D,
				94.25D, 93.7D, 92.22D, 92.48D, 92.12D, 91.08D, 89.81D, 90.43D, 90.77D, 89.14D, 89.38D,
				87.73D, 86.75D, 86.62D, 86.39D, 86.37D, 85.36D, 84.12D, 84.84D, 84.82D, 84.69D, 83.65D,
				84D, 84.62D, 84.92D, 85.85D, 84.65D, 84.5D, 84.3D, 84.62D, 84.87D, 81.71D, 81.11D,
				74.96D, 75.96D, 75.88D, 74.99D, 74.14D, 73.99D, 74.53D, 74.23D, 74.78D, 75.76D, 74.78D,
				74.78D, 75.97D, 76.97D, 77.51D, 77.38D, 76.68D, 76.69D, 76.78D, 77.11D, 77.86D, 77.03D,
				76.12D, 75.53D, 75.89D, 75.91D, 75.25D, 74.96D, 75.81D, 76.66D, 76.58D, 75.85D, 75.78D,
				75.82D, 76.05D, 75.89D, 75.39D, 75.18D, 75.38D, 73.91D, 74.58D, 75.36D, 75.04D, 75.88D,
				76.77D, 78D, 77.71D, 77.78D, 76.56D, 76.57D, 75.57D, 74.24D, 73.22D, 73.23D, 72.68D,
				71.65D, 71.51D, 71.4D, 71.54D, 72.36D, 78.64D, 78.01D, 79.45D, 78.79D, 78.44D, 77.24D,
				79.18D, 79.62D, 78.06D, 76.53D, 76.13D, 76.65D, 77.64D, 77.15D, 77.7D, 77.28D, 79.02D,
				80.15D, 79.22D, 80.01D, 80.56D, 81.1D, 81.44D, 78.43D, 77.78D, 78.68D, 79.28D, 79.64D,
				79.2D, 80.08D, 80.19D, 80.79D, 80.92D, 80D, 81.13D, 80.71D, 80.9D, 78.75D, 79.44D,
				77.99D, 76.2D, 74.82D, 74.26D, 74.45D, 73.57D, 74.22D, 74.09D, 75D, 75.45D, 74.38D,
				74.29D, 74.15D, 74.37D, 73.21D, 74.42D, 75.06D, 75.25D, 74.29D, 74.67D, 74.98D, 73.81D,
				75.7D, 75.14D, 75.99D, 74.99D, 74.27D, 74.48D, 72.7D, 72.07D, 71.59D, 71.24D, 70.86D, 70.4D,
				69.95D, 69.51D, 68.71D, 69.68D, 69D, 69.06D, 69.94D, 69.71D, 68.11D, 68.96D, 69.46D, 68.79D,
				69.87D, 70.09D, 66.77D, 67.47D, 66.38D, 65.05D, 64.3D, 66.41D, 67.53D, 66.82D, 70.66D, 72.3D
			};

			if (sortAsc)
				closePrices.Reverse ();
			return closePrices;
		}

		#endregion
	}
}

