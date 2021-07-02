$(document).ready(function(){
    var id=$(location).attr('href').split("=")[1];

    $('#updatebtn').click(function(){
		updateDrinks();
	});

    var updateDrinks=function(){
        if($("#drinksName").val()==""||$("#drinksQuantity").val()==""||$("#drinksUnitPrice").val()==""){
            $("#errormsg1").html("All Fields Required");
        }
        else{
		console.log("happy");
		$.ajax({
		url:"http://localhost:5278/api/colddrinks/"+id,
		method:"PUT",
		header:"Content-Type:application/json",
		data:{
            coldDrinksId:$("#drinksId").val(),
            coldDrinksName:$("#drinksName").val(),
            quantity:$("#drinksQuantity").val(),
            unitPrice:$("#drinksUnitPrice").val()
		},
		complete:function(xmlhttp,status){
			if(xmlhttp.status==200)
			{
				window.location="/API_Ibos/Home.html";
			}
			else
			{
				console.log(xmlhttp.status+":"+xmlhttp.statusText);
			}
		}
	});
}
    }
 
    console.log(id);
    var editColdDrinks=function(){
		$.ajax({
		url:"http://localhost:5278/api/colddrinks/"+id,
		method:"GET",
		complete:function(xmlhttp,status){
			if(xmlhttp.status==200)
			{
				var data=xmlhttp.responseJSON;
                console.log(data);
				$("#drinksId").val(data.coldDrinksId);
                $("#drinksName").val(data.coldDrinksName);
                $("#drinksQuantity").val(data.quantity);
                $("#drinksUnitPrice").val(data.unitPrice);
			}
			else
			{
				$("#mike2").html(xmlhttp.status+":"+xmlhttp.statusText);
			}
		}
	});

	}



    editColdDrinks();
});