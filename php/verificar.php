<?php
if(isset($_GET['ids']) && isset($_GET['cod']) && isset($_GET['lic']))
{
    $ids = $_GET['ids'];
    $cod = $_GET['cod'];
    $lic = $_GET['lic'];

    $server = 'localhost';
    $user = 'Logikoz';
    $pass = '123';
    $db = 'projects';

    try
    {
        $pdo = new PDO("mysql:host=$server; dbname=$db; charset=utf8;", $user, $pass);

        $select = $pdo->prepare("select count(*) from clientes where binary licence = ? && cod_cliente = ? && id_software = ?");
        $select -> bindParam(1, $lic);
        $select -> bindParam(2, $cod);
        $select -> bindParam(3, $ids);

        if ($select -> execute() && $select->fetchColumn() == 1)
        {
            echo "server=$server;userid=$user;pwd=$pass;port=3306;database=$db";
        }
        else
        {
            echo "no";
        }
    }
    catch(PDOException  $e )
    {
        echo "Error: ".$e;
    }
}
else
{
    echo "Nao pode deixar nenhum dos campos vazios!";
}
?>