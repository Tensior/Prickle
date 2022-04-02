using UI;

namespace Core.Managers
{
	public class PointManager
	{
		private readonly ScoreVM _scoreVM;
		private int _points;

		public int Points
		{
			get => _points;
			private set
			{
				_points = value;
				_scoreVM.Score = _points;
			}
		}

		public PointManager(ScoreVM scoreVM)
		{
			_scoreVM = scoreVM;
		}

		public void Reset()
		{
			Points = 0;
		}

		public void ResetPoints(int points)
		{
			Points = points;
		}

		public void AddPoints(int pointsToAdd)
		{
			Points += pointsToAdd;
		}
	}
}

