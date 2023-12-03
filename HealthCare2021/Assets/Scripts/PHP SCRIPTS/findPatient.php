<?php

$con = mysqli_connect('192.168.0.100', 'pantera1_alsivic', '81(wE(pzS9', 'pantera1_alsivic');

// Check if the connection happened
if (mysqli_connect_errno()) {
    echo "1: Connection failed"; // error code #1 = connection failed
    exit();
}

$patient_name = $_POST['patient_name'];
$doctor_id = $_POST['doctor_id'];

 echo "Received doctor id: $doctor_id\n";

// Search for the patient in the users table (case-insensitive)
$query = "SELECT patientID FROM users WHERE LOWER(name) = LOWER('$patient_name')";
$result = mysqli_query($con, $query);

if ($result) {
    if (mysqli_num_rows($result) > 0) {
        $row = mysqli_fetch_assoc($result);
        $patient_id = $row['patientID'];

        // Debug statement
        //echo "Patient ID: $patient_id\n";

        // Check for the connection in the connections table
        $checkQuery = "SELECT doctorID, patientID FROM connections WHERE doctorID = '$doctor_id' AND patientID = '$patient_id'";
        $checkResult = mysqli_query($con, $checkQuery);

        if ($checkResult && mysqli_num_rows($checkResult) > 0) {
            echo "Connection found";
        } else {
            echo "2: Connection not found";
        }
    } else {
        echo "3: Patient not found";
    }
} else {
    echo "5: Error searching for patient: " . mysqli_error($con);
}

mysqli_close($con);
?>
