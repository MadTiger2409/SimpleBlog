using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleBlog.Extensions.PdfRaport
{
    public static class RaportTemplate
    {
        public static string GetHTMLTemplate(RaportStatistics statistics)
        {
            var stringBuilder = new StringBuilder();

            stringBuilder.AppendFormat(@"
                        <html>
                            <head>
                            </head>
                            <body>
                                <div class='header'><h1>SimpleBlog raport for {0}</h1></div>
                                <table align='center'>
                                    <tr>
                                        <th>Property</th>
                                        <th>Value</th>
                                    </tr>
                                    <tr>
                                        <td>Posts count</td>
                                        <td>{1}</td>
                                    </tr>
                                    <tr>
                                        <td>Comments count</td>
                                        <td>{2}</td>
                                    </tr>
                                    <tr>
                                        <td>Date of the oldest post</td>
                                        <td>{3}</td>
                                    </tr>
                                </table>
                            </body>
                        </html>", DateTime.UtcNow, statistics.PostsCount, statistics.CommentsCount, statistics.OldestPostDate);

            return stringBuilder.ToString();
        }
    }
}
