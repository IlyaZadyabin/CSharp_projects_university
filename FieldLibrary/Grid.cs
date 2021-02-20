using System;

namespace FieldLibrary
{
    [Serializable]
    public struct Grid {
        public Grid(float t0_, float time_step_, int amount_of_nodes_) {
            t0 = t0_;
            time_step = time_step_;
            amount_of_nodes = amount_of_nodes_;
        }

        public override string ToString() {
            return t0.ToString() + " " + time_step.ToString() + " " + amount_of_nodes.ToString();
        }
        public float t0 { get; set; }
        public float time_step { get; set; }
        public int amount_of_nodes { get; set; }
        public string ToString(string format) {
            return t0.ToString(format) + " " + time_step.ToString(format) + " " + amount_of_nodes.ToString(format);
        }
    }
}
