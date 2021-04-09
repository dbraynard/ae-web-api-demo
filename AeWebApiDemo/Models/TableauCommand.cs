using System;
using System.Linq;
using System.Text.Json;
using AeWebApiDemo.Query;
using System.Threading.Tasks;

namespace AeWebApiDemo.Models {
    public class TableauCommand {
        public string Script { get; set; }
        public TableauData Data { get; set; }
        public IQuery Query { get; }

        public TableauCommand(string script, IQuery query, TableauData data) {
            Script = script;
            Query = query;
            Data = data;
        }

        async public Task<object[]> ExecuteAsync() => await Query.ExecuteAsync(Data);

        public static TableauCommand GetFromJsonElement(JsonElement json) {
            var script = json.GetProperty(TableauKeywords.Script).ToString();
            var dataJson = json.GetProperty(TableauKeywords.Data);
            var query = QueryFactory.GetQuery(script);
            var tableauData = new TableauData();
            for (var i = 0; i < query.Types.Length; i++) {
                var argName = TableauKeywords.Arg(i);
                var argJson = dataJson.GetProperty(argName);
                var argType = query.Types[i];
                var dataField = CreateTableauDataField(argJson, argType);
                tableauData.Add(argName, dataField);
            }

            return new TableauCommand(script, query, tableauData);
        }

        private static TableauDataField CreateTableauDataField(JsonElement argJson, Type argType) {
            if (argType == typeof(string)) {
                var data = argJson.EnumerateArray().Select(x => x.GetString()).ToArray();
                var dataField = new TableauDataField(data);
                return dataField;
            } else if (argType == typeof(int)) {
                var data = argJson.EnumerateArray().Select(x => x.GetInt32()).ToArray();
                var dataField = new TableauDataField(data);
                return dataField;
            } else if (argType == typeof(double)) {
                var data = argJson.EnumerateArray().Select(x => x.GetDouble()).ToArray();
                var dataField = new TableauDataField(data);
                return dataField;
            } else {
                throw new NotSupportedException($"Type: {argType} is not supported "
                + "as a data field.");
            }
        }
    }
}