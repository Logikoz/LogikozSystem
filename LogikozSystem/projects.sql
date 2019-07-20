use projects;
create database projects;
truncate table licences;
truncate table clientes;

select * from licences; #order by id limit 1; #mostrar dados da tabela
select * from clientes;
select cod_cliente, licence, passconfig, id_software from clientes;
select * from usuarios;
select xp_atual_user, nivel_atual_user, minutos_registrados_user from usuarios;

#testes
#----------------------------------------------------------------------------------------------------------------------------------
select count(*) from clientes where binary licence = "Swoxa-4c2f8-8Nwop-n&0Cv-Dn#wi" and cod_cliente = "68888165";

SELECT day(STR_TO_DATE("2000-06-18", "%Y-%m-%d")) FROM usuarios;
select day(str_to_date(data_nascimento_user, "%Y-%m-%d")) from usuarios where login_user = 'Logikoz';

show tables;
truncate table codigosrecuperar;
select * from codigosrecuperar;
update usuarios set senha = 123 where email_user = "ruancarlos14@hotmail.com";

insert into codigosrecuperar (id, codigo) values (null, 1234);

select count(*) from clientes where cod_cliente = '68888165' && binary licence = 'Swoxa-4c2f8-8Nwop-n&0Cv-Dn#wi';

update clientes set maintenance_bool = 'OFF' where cod_cliente = 27107117;

create table if not exists dados(servidor varchar(30), usuario varchar(100), senha varchar(100), porta integer);

select * from clientes where maintenance_bool = 'ON' && cod_cliente = 5435345345;

create table if not exists teste (id integer not null auto_increment primary key);
drop table if exists teste;

update clientes set licence = '71c82-TNFwB-kcPwE-zxt%L-VSkRa' where cliente = 'Coopetta';

select Count(*) from licences where cod_cliente = 43246543 || binary cliente = "coopetta";

delete from licences where cod_cliente = 43246543;

INSERT INTO `licences` (`id`, `cod_cliente`, `cliente`, `licence`, `days`, `data_criacao`, `data_remocao`, `suspenso_bool`, `maintenance_bool`, `menssage`, `backSystem_prev`, `atualization_bool`, `link_att`, `email_cliente`) VALUES (null, 12345678, 'coopetta', 'g4D1v-39kcb-KfMfm-aJpjH-o65u3-wtHeT', '30', '2019-02-21', '', 'OFF', 'OFF', 'teste', 'sabado, 20 de abril de 2019 as 20:30', 'OFF', 'google.com.br', 'coopetta@outlook.com.br');

#----------------------------------------------------------------------------------------------------------------------------------

#drop table usuarios;
create table usuarios(
id integer not null auto_increment primary key,
nivel_acesso_user integer not null,
login_user varchar(30) not null,
senha_user varchar(100) not null,
nome_user varchar(100),
cargo_user varchar(100),
email_user varchar(50) not null,
data_nascimento_user varchar(10),
nivel_atual_user integer default 0,
numero_clientes_user integer default 0,
minutos_registrados_user integer default 0,
xp_atual_user double default 0,
recado_user varchar(120),
logradouro_user varchar(100),
numero_casa_user integer,
cidade_user varchar(30),
estado_user varchar(30),
pais_user varchar(30),

foto_user mediumblob);

create table clientes(
id integer not null auto_increment primary key,
cod_cliente integer not null,
cliente varchar(30) not null,
licence varchar(40) not null,
days integer not null not null,
data_criacao varchar(10) not null,
data_remocao varchar(10),
suspenso_bool char(3),
maintenance_bool char(3),
menssage varchar(500),
backsystem_prev varchar(50),
atualization_bool char(7),
link_att varchar(50),
email_cliente varchar(30) not null,
contato varchar(15) not null,
passconfig varchar(100) not null,
suspenso_msg varchar(500),
produto_desc varchar(200),
id_software varchar(128));


create table licences(
id integer not null auto_increment primary key,
use_bool char(3) not null,
licence varchar(30) not null);

#drop table codigosrecuperar;
create table codigosrecuperar(
id integer not null primary key auto_increment,
codigo int,
email varchar(100));
