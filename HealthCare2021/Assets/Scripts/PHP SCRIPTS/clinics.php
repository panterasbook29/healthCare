<?php

$con = mysqli_connect('192.168.0.100', 'pantera1_alsivic', '81(wE(pzS9', 'pantera1_alsivic');

if(mysqli_connect_errno()) {
    echo "1: connection failed"; //error code #1 = connection failed
    exit();
}

$code = $_POST["clinicCode"];

$codeQuery = "SELECT code, name FROM clinics WHERE code='". $code ."';";
$codeCheck = mysqli_query($con, $codeQuery) or die("2: code check query failed"); //error code #2 - code check query failed

$clinic = mysqli_fetch_assoc($codeCheck);

if(empty($clinic)) {
    echo "5: No user with the given code";
} else {
    echo json_encode($clinic);
}

$con->close();

?>