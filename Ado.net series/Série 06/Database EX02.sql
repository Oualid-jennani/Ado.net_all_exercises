create database ADO_serie6
use ADO_serie6

create table Client( nunC smallint primary key identity , nom varchar(30) , prenom varchar(30)  )
create table Produit(numP smallint primary key identity , libelle varchar(30) , pu smallmoney)
create table Commande(numC smallint foreign key references Client( nunC), numP smallint foreign key references Produit(numP), quantite smallint  )

insert into Client values( 'walid','jenani' ),( 'youssef','mossawi' ),( 'alae','jenani' )
insert into Produit values('stylo',30),('livre',20),('table',50)
insert into Commande values (1,1,9),(1,2,11),(1,3,20),(3,2,51),(2,1,6),(2,3,30),(3,1,20)

select * from Client