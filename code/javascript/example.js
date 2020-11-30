// npm install --save neo4j-driver
// node example.js
const neo4j = require('neo4j-driver');
const driver = neo4j.driver('neo4j+s://demo.neo4jlabs.com:7687',
                  neo4j.auth.basic('graph-data-science', 'graph-data-science'), 
                  {/* encrypted: 'ENCRYPTION_OFF' */});

const query =
  `
  MATCH (c:Person{name:$name})-[r:INTERACTS]->(other)
  RETURN other.name as person
  `;

const params = {"name": "Jaime Lannister"};

const session = driver.session({database:"graph-data-science"});

session.run(query, params)
  .then((result) => {
    result.records.forEach((record) => {
        console.log(record.get('person'));
    });
    session.close();
    driver.close();
  })
  .catch((error) => {
    console.error(error);
  });
