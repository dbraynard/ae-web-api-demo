namespace AeWebApiDemo.Models {
    public static class TableauKeywords {
        public static string Script = "script";
        public static string Data = "data";
        public static string Arg(int i) => $"_arg{i + 1}";
    }
}