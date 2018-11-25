using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FlowChecker
{
    class Downloader
    {
        public Downloader()
        {
        }

        public async Task<string> GetLastRow(string url, string rowSelector)
        {
            try
            {
                HtmlDocument doc = await LoadHtmlDocument(url);
                HtmlNode tableRow = doc.DocumentNode.SelectSingleNode(rowSelector);

                return GetCsvLineFromRow(tableRow);
            }
            catch (Exception ex)
            {
                return String.Empty;
            }
        }

        public async Task<string[]> GetLastRows(string url, string rowsSelector)
        {
            try
            {
                HtmlDocument doc = await LoadHtmlDocument(url);
                HtmlNodeCollection tableRows = doc.DocumentNode.SelectNodes(rowsSelector);
                return tableRows.Select(row => GetCsvLineFromRow(row)).ToArray();
            }
            catch (Exception ex)
            {
                return new string[] { };
            }
        }

        private async Task<HtmlDocument> LoadHtmlDocument(string url)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(url);

            Stream stream = await response.Content.ReadAsStreamAsync();
            HtmlDocument doc = new HtmlDocument();
            doc.Load(stream);

            return doc;
        }

        private string GetCsvLineFromRow(HtmlNode row)
        {
            StringBuilder str = new StringBuilder();
            foreach(HtmlNode col in row.ChildNodes)
            {
                if (col.InnerText.Trim() != "")
                {
                    str.Append(col.InnerText.Trim());
                    str.Append(';');
                }
            }
            str.Remove(str.Length - 1, 1);
            return str.ToString().Trim(new[] {';'});
        }
    }
}
