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
		//TODO: If the plan is, instead, a jagged polylist, each level can indicate units that can be built concurrently or even distributively. Respectively, a higher level or lower level would indicate dependent upon or dependency of (potentially). This would offer potentially huge improvements to build time, especially as I've noticed over the years that the GNAT planner is deficient. This feature is low on the priority list currently, as the build system needs to actually _work_ first, and the major benefit to structuring the build plan that way only shows up on massively parallel systems. Either a large number of cores or a collection of build servers is needed; this requires an actual build service/daemon be built.

		/// <summary>
		/// The actual plan itself
		/// </summary>
		private readonly List<Unit> Plan;

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

		/// <summary>
		/// Get the length of the build plan; the amount of steps to be made
		/// </summary>
		public Int32 Length { get => this.Plan.Count; }

		IEnumerator IEnumerable.GetEnumerator() => new BuildPlanEnumerator(this.Plan);

		IEnumerator<Unit> IEnumerable<Unit>.GetEnumerator() => new BuildPlanEnumerator(this.Plan);

		public BuildPlan() {
			this.Plan = new List<Unit>();
		}

		/// <summary>
		/// Construct a build plan for the specified project
		/// </summary>
		/// <param name="Project">Project to construct a build plan for</param>
		public BuildPlan(Project Project) : this(Project.Units) {
		}

		public BuildPlan(params Unit[] Units) : this((IEnumerable<Unit>)Units) {
		}

		public BuildPlan(IEnumerable<Unit> Units) {
			this.Plan = new List<Unit>();
			foreach (Unit Unit in Units) {
				this.Add(Unit);
			}
		}

	}
}
