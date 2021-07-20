select * from commande c where c.numCom in
 (
 select lc.numCom from  ligneCommande lc inner join produit p on lc.numP=p.numP
 group by lc.numCom  having sum(p.pu*lc.qt) =(select max(maxc) from ( 
 select sum(PR.pu*LIC.qt)as "maxc" from produit PR inner join ligneCommande LIC on PR.numP=LIC.numP GROUP BY LIC.numCom ) as "table") 
 )

select * from commande where numCom in (
select LIC.numCom from ligneCommande LIC inner  join   produit pr on pr.numP=LIC.numP group by LIC.numCom 
having sum(pr.pu*LIC.qt) in(
select Top(1)sum(p.pu*lc.qt)as somme from  ligneCommande lc 
inner join produit p on p.numP=lc.numP 
group by lc.numCom order by somme desc)
)


------------------------------------------------------------------------------------------------
select * from commande c where c.numCom in
(
select lc.numCom from  ligneCommande lc inner join produit p on lc.numP=p.numP  where p.numP =2
 group by lc.numCom having (select count(*) from ligneCommande where numCom=lc.numCom)=1
)





select * from client where numC in 
(
select c.numC from commande c inner join ligneCommande lc on lc. numCom = c.numCom  group by c.numC
having count(distinct(lc.numP)) = (select count(*)from produit)
)

select * from client where numC in 
(
	select c.numC from commande c inner join ligneCommande lc on lc. numCom = c.numCom  group by c.numC
	having count(distinct(lc.numP)) = (select max(MX) from
	(select count(distinct(lc.numP)) as MX  from commande c inner join ligneCommande lc on lc.numCom = c.numCom group by c.numC) as "table")
)