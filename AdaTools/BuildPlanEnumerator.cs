using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace AdaTools {
	public class BuildPlanEnumerator : IEnumerator<Unit> {

		private readonly Unit[] Plan;

		private Int32 Index = -1;

		public Unit Current { get => this.Plan[this.Index]; }

		Object IEnumerator.Current { get => this.Plan[this.Index]; }

		/// <summary>
		/// There's nothing to actually dispose of, everything is managed
		/// </summary>
		public void Dispose() { }

		public Boolean MoveNext() {
			this.Index++;
			if (this.Index < this.Plan.Length) {
				return true;
			} else {
				return false;
			}
		}

		public void Reset() => this.Index = -1;

		public BuildPlanEnumerator(List<Unit> Plan) {
			this.Plan = Plan.ToArray();
		}

	}
}
