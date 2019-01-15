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

        public async Task<List<dynamic>> GetLastRow(string url, string rowSelector)
        {
            try
            {
                HtmlDocument doc = await LoadHtmlDocument(url);
                HtmlNode tableRow = doc.DocumentNode.SelectSingleNode(rowSelector);

                return GetLineFromRow(tableRow);
            }
            catch (Exception ex)
            {
                return new List<dynamic>(0);
            }
        }

        public async Task<List<dynamic>[]> GetLastRows(string url, string rowsSelector)
        {
            try
            {
                HtmlDocument doc = await LoadHtmlDocument(url);
                HtmlNodeCollection tableRows = doc.DocumentNode.SelectNodes(rowsSelector);
                return tableRows.Select(row => GetLineFromRow(row)).ToArray();
            }
            catch (Exception ex)
            {
                return new List<dynamic>[] { };
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

        private List<dynamic> GetLineFromRow(HtmlNode row)
        {
            List<dynamic> result = new List<dynamic>();
            foreach (HtmlNode col in row.ChildNodes)
                if (col.InnerText.Trim() != "")
                    result.Add(col.InnerText.Trim());

            return result;
        }
    }
}
