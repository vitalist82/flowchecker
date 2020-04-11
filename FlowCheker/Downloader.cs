using FlowCheker;
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
    public class Downloader
    {
        public Downloader()
        {
        }

        public async Task<List<string>> GetLastRow(string url, string rowSelector)
        {
            try
            {
                HtmlDocument doc = await LoadHtmlDocument(url);
                HtmlNode tableRow = doc.DocumentNode.SelectSingleNode(rowSelector);

                return GetLineFromRow(tableRow);
            }
            catch (Exception ex)
            {
                return new List<string>(0);
            }
        }

        public async Task<List<string>[]> GetLastRows(string url, string rowsSelector)
        {
            Logger.Log(LogLevel.Debug, "Getting data from " + url);
            try
            {
                HtmlDocument doc = await LoadHtmlDocument(url);
                HtmlNodeCollection tableRows = doc.DocumentNode.SelectNodes(rowsSelector);
                Logger.Log(LogLevel.Debug, "Finished downloading data from " + url);
                return tableRows.Select(row => GetLineFromRow(row)).ToArray();
            }
            catch (Exception ex)
            {
                Logger.Log(LogLevel.Error, "GetLastRows failed with an exception: " + ex.Message);
                return new List<string>[] { };
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

        private List<string> GetLineFromRow(HtmlNode row)
        {
            List<string> result = new List<string>();
            foreach (HtmlNode col in row.ChildNodes)
                if (col.InnerText.Trim() != "")
                    result.Add(col.InnerText.Trim());

            return result;
        }
    }
}
