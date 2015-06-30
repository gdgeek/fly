using UnityEngine;
using System.Collections;
using GDGeek;

namespace VoxelTrek
{
	public class UIScore : MonoBehaviour {


		public VoxelText _scoreText;
		public VoxelText _score;
		public VoxelText _bestText;
		public VoxelText _best;
		public Task show(int score, int best){
			TaskList tl = new TaskList ();
			tl.push (_scoreText.setTextTask("SCORE"));
			tl.push (_score.setTextTask(score.ToString()));
			tl.push (_bestText.setTextTask("BEST"));
			tl.push (_best.setTextTask(best.ToString()));
			return tl;
		}

		public Task clearTask(){
			TaskSet ts = new TaskSet ();
			ts.push (_scoreText.setTextTask(""));
			ts.push (_score.setTextTask(""));
			ts.push (_bestText.setTextTask(""));
			ts.push (_best.setTextTask(""));
			return ts;
		}
		public void clear ()
		{
			_scoreText.setText ("");
			_score.setText ("");
			_bestText.setText ("");
			_best.setText ("");
		}
	}
}
