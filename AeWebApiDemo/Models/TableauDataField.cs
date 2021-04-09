namespace AeWebApiDemo.Models {
    public class TableauDataField {
        double[] doubleData;
        public double[] DoubleData => doubleData;
        string[] stringData;
        public string[] StringData => stringData;
        int[] intData;
        public int[] IntData => intData;

        public TableauDataField(double[] doubleData) {
            this.doubleData = doubleData;
        }

        public TableauDataField(int[] intData) {
            this.intData = intData;
        }

        public TableauDataField(string[] stringData) {
            this.stringData = stringData;
        }
    }
}