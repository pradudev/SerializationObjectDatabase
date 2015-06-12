# Different ways to serialize C# objects and store it in SQL Server

There are situation where we may need to store C# objects into Database and then retrive it later.
Ex: If an application has a BO that represents complete user data for a particular module and say if the module as many pages like wizards. 
We may give user option to save partially filled application form in DB and then can retrive them later when user returns next day (like UK Visa application form).


Out of all the three storage options I would prefer using XML because:
* XML column can be queried in SQL
* Size of serialed data is very less as compare to other two options

Base64 takes more time to serialize and deserialize instead use Binary one. 

There are other data types like BLOB & Image which is equivalent to VarBinary(MAX).
