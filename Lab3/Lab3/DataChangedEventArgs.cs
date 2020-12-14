using System;
using System.Collections.Generic;
using System.Text;

namespace Lab3
{
    public class DataChangedEventArgs {
        public DataChangedEventArgs(ChangeInfo change_, string info_) {
            Change = change_;
            info = info_;
        }   
        public ChangeInfo Change { get; set; }

        public string info { get; set; }

        public override string ToString() {
            return "ChangeInfo: " + Change.ToString() + Environment.NewLine + info.ToString();
        }
    }
}
