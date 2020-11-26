// install dotnet core on your system
// dotnet new console -o .
// dotnet add package Neo4j.Driver
// paste in this code into Program.cs
// dotnet run

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Neo4j.Driver;
  
namespace dotnet {
  class Example {
  static async Task Main() {
    var driver = GraphDatabase.Driver("neo4j+s://demo.neo4jlabs.com:7687", 
                    AuthTokens.Basic("graph-data-science", "graph-data-science"));

    var cypherQuery =
      @"
      MATCH (c:Person{name:$name})-[r:INTERACTS]->(other) 
        RETURN other.name as person
      ";

    var session = driver.AsyncSession(o => o.WithDatabase("graph-data-science"));
    var result = await session.ReadTransactionAsync(async tx => {
      var r = await tx.RunAsync(cypherQuery, 
              new { name="Jaime Lannister"});
      return await r.ToListAsync();
    });

    await session?.CloseAsync();
    foreach (var row in result)
      Console.WriteLine(row["person"].As<string>());
	  
    }
  }
}