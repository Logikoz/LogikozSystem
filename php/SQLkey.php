<?php
if(isset($_GET['cod']) && isset($_GET['pwd']))
{
    $cod = $_GET['cod'];
    $pwd = $_GET['pwd'];

    $server = 'localhost';
    $user = 'Logikoz';
    $pass = '123';
    $db = 'projects';

    try
    {
        $pdo = new PDO("mysql:host=$server; dbname=$db; charset=utf8;", $user, $pass);

        $post = $pdo->prepare("select id_software from clientes where binary passconfig = ? && cod_cliente = ?");
        $post -> bindParam(1, $pwd);
        $post -> bindParam(2, $cod);
        if($post ->execute() && $row = $post->fetch())
        {
            echo $row['id_software'];
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