using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace AdaTools {
	/// <summary>
	/// Represents the build plan; the order in which units must be built
	/// </summary>
	/// <remarks>
	/// <para>This is, essentially, a priority queue. The exact implementation may change, but it will always semantically be a priority queue.</para>
	/// </remarks>
	public sealed class BuildPlan : IEnumerable<Unit> {

		private List<Unit> Plan;

		/// <summary>
		/// Add the <paramref name="Unit"/> to the appropriate order in the plan
		/// </summary>
		/// <param name="Unit">Compilation unit to add</param>
		public void Add(Unit Unit) {
			for (Int32 i = 0; i < this.Plan.Count; i++) {
				if (this.Plan[i].DependsOn(Unit)) {
					this.Plan.Insert(i, Unit);
					return;
				}
			}
			// Nothing depends on this unit, so we can place it at the end
			this.Plan.Add(Unit);
		}

		public Int32 Length { get => this.Plan.Count; }

		public Unit this[Int32 Index] {
			get => this.Plan[Index];
		}

		#region IEnumerable
		IEnumerator IEnumerable.GetEnumerator() => new BuildPlanEnumerator(this.Plan);

		IEnumerator<Unit> IEnumerable<Unit>.GetEnumerator() => new BuildPlanEnumerator(this.Plan);
		#endregion IEnumerable

		public BuildPlan() {
			this.Plan = new List<Unit>();
		}

		/// <summary>
		/// Construct a build plan for the specified project
		/// </summary>
		/// <param name="Project">Project to construct a build plan for</param>
		public BuildPlan(Project Project) : this(Project.Units) {
		}

		public BuildPlan(params Unit[] Units) {
			this.Plan = new List<Unit>(Units);
		}

		public BuildPlan(IEnumerable<Unit> Units) {
			this.Plan = new List<Unit>();
			foreach (Unit Unit in Units) {
				this.Add(Unit);
			}
		}

	}
}
