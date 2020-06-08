<?php

	$con = mysqli_connect("oege.ie.hva.nl", "baasdr", "l#aEkJ7cojymzj", "zbaasdr", "3306");
	if (mysqli_connect_errno()) {
		echo "1";
		exit();
	}
	
	$username = $_POST["name"];
	$score = $_POST["score"];
	
	// queries
	$checkData = "SELECT * FROM Player WHERE username='" . $username . "';";
	$insertData = "INSERT INTO Player (username, score) VALUES ('" . $username . "', '" . $score . "');"
	$updateData = "UPDATE Player SET score='" . $score . "' WHERE username='" . $username . "';";
	
	$checking = mysqli_query($con, $checkData) or die("2: Query Failed");
	if (mysqli_num_rows($checking) > 0) {
		if (mysqli_query($con, $updateData)) {
			echo "Updated data!";
		} else {
			echo "Could not update data!";
		}
	} else {
		if (mysqli_query($con, $insertData)) {
			echo "Inserted data!";
		} else {
			echo "Could not insert data!";
		}
	}
	echo "0";
	mysqli_close();
?>