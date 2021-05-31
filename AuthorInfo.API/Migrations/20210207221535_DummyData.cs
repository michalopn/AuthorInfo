using Microsoft.EntityFrameworkCore.Migrations;

namespace AuthorInfo.API.Migrations
{
    public partial class DummyData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "Id", "Name", "ShortBio" },
                values: new object[] { 1, "Ian Rankin", "Ian James Rankin (born 28 April 1960) is a Scottish crime writer, best known for his Inspector Rebus novels." });

            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "Id", "Name", "ShortBio" },
                values: new object[] { 2, "Philip Kerr", "Philip Ballantyne Kerr (22 February 1956 – 23 March 2018) was a British author, best known for his Bernie Gunther series of historical detective thrillers." });

            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "Id", "Name", "ShortBio" },
                values: new object[] { 3, "Adrian McKinty", "Adrian McKinty is a Northern Irish writer of crime and mystery novels and young adult fiction, best known for his Sean Duffy novels set in Northern Ireland during The Troubles." });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "AuthorId", "Synopsis", "Title" },
                values: new object[,]
                {
                    { 1, 1, "Knots and Crosses is a 1987 crime novel by Ian Rankin. It is the first of the Inspector Rebus novels. It was written while Rankin was a postgraduate student at the University of Edinburgh.", "Knots and Crosses" },
                    { 2, 1, "Hide and Seek is a 1991 crime novel by Ian Rankin. It is the second of the Inspector Rebus novels.", "Hide and Seek" },
                    { 3, 2, "March Violets is a historical detective novel and the first written by Philip Kerr featuring detective Bernhard \"Bernie\" Gunther. March Violets is the first of the trilogy by Kerr called Berlin Noir.", "March Violets" },
                    { 4, 2, "The Pale Criminal is a historical detective novel and the second in the Berlin Noir trilogy of Bernhard Gunther novels written by Philip Kerr.", "The Pale Criminal" },
                    { 5, 3, "Dead I Well May be is a 2003 novel by Irish/Australian author Adrian McKinty. It is his second novel, following Orange Rhymes With Everything, and was nominated for the CWA Ian Fleming Steel Dagger award for the best thriller of the year.", "Dead I Well May Be" },
                    { 6, 3, "In the Morning I'll be Gone is a 2014 novel by Belfast born novelist Adrian McKinty which won the 2014 Ned Kelly Award for Best Novel. It is the third in the author's Sean Duffy series, following The Cold Cold Ground and I Hear the Sirens in the Street.", "In the Morning I'll Be Gone" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
