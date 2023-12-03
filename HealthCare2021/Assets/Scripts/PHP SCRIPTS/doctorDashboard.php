<?php

$con = mysqli_connect('192.168.0.100', 'pantera1_alsivic', '81(wE(pzS9', 'pantera1_alsivic');

// Check if the connection happened
if (mysqli_connect_errno()) {
    echo "1: connection failed"; // error code #1 = connection failed
    exit();
}

$doctor_id = $_POST['doctor_id'];

// Fetch connected patients
$query = "SELECT * FROM connections WHERE doctor_id = '$doctor_id'";
$result = mysqli_query($conn, $query);

// Process the result (convert to JSON, for example)
$connections = array();
while ($row = mysqli_fetch_assoc($result)) {
    $connections[] = $row;
}

echo json_encode($connections);

// Close database connection
mysqli_close($conn);

?>