<?php
if(isset($_GET['lic']))
{
    $lic = $_GET['lic'];

    $server = 'localhost';
    $user = 'Logikoz';
    $pass = '123';
    $db = 'projects';

    try
    {
        $pdo = new PDO("mysql:host=$server; dbname=$db; charset=utf8;", $user, $pass);

        $post = $pdo->prepare("select count(*) from licences where binary licence = ?");
        $post -> bindParam(1, $lic);
        if($post ->execute() && $post -> fetchColumn() == 1)
        {
            echo "yes";
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