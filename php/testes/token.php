<?php
if(isset($_GET['login']) && isset($_GET['senha']) && isset($_GET['cod']))
{
    $login = $_GET['us'];
    $senha = $_GET['pwd'];
    $token = $_GET['tk'];

    $server = 'localhost';
    $user = 'Logikoz';
    $pass = '123';
    $db = 'projects';

    $metodo = 'aes-256-cbc';


    //$con = new PDO("mysql:host=$server; dbname=$db; charset=utf8;", $user, $pass);

    //$token = $con->query("")
}
else
{
    echo "Nao pode deixar nenhum dos campos vazios!";
}
?>