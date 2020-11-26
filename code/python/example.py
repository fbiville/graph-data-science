# pip3 install neo4j-driver
# python3 example.py

from neo4j import GraphDatabase, basic_auth

driver = GraphDatabase.driver(
  "neo4j+s://demo.neo4jlabs.com:7687", 
  auth=basic_auth("graph-data-science", "graph-data-science"))

cypher_query = '''
MATCH (c:Person{name:$name})-[r:INTERACTS]->(other) 
  RETURN other.name as person
'''

with driver.session(database="graph-data-science") as session:
  results = session.read_transaction(
    lambda tx: tx.run(cypher_query,
      name="Jaime Lannister").data())

  for record in results:
    print(record['person'])

driver.close()