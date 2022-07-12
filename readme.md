# Mongo DB
Testovací projekt pro ověření funkčnosti a k získání znalostí.

Databáze stažena jako Docker image: https://hub.docker.com/_/mongo

První implementace provedena podle:  
https://putridparrot.com/blog/creating-a-simple-database-in-mongodb-with-c/  
https://www.c-sharpcorner.com/article/getting-started-with-mongodb-mongodb-with-c-sharp/

### Oficiální dokumentace
https://docs.mongodb.com/manual/

#### Dokumentace k C# driveru
https://mongodb.github.io/mongo-csharp-driver/

### Články o Mongo DB
Pěkný 3-dílný článek:  
http://www.cloudsvet.cz/?p=249

## Import
```mongoimport Citi_Bike_trip_data.csv -d pilifs -c citibike --type csv --headerline```