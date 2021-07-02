$(document).ready(function(){
    $("#mike").html("Cold Drinks List");
    $('#here').click(function(){
		deleteDrinks();
	});
    var deleteDrinks=function(){
        console.log("delete");
		$.ajax({
		url:"http://localhost:5278/api/colddrinks/",
		method:"Delete",
		complete:function(xmlhttp,status){
			if(xmlhttp.status==204 )
			{
				loadList();
                loadtotalprice();
			}
			else
			{
				console.log(xmlhttp.status+":"+xmlhttp.statusText);
			}
		}
	});	
	}


    var loadList=function(){
		$.ajax({
		url:"http://localhost:5278/api/colddrinks",
		method:"GET",
		complete:function(xmlhttp,status){
			if(xmlhttp.status==200)
			{
				var data=xmlhttp.responseJSON;
                console.log(data);
				var str="";
				for (var i = 0; i < data.length; i++) {
					str+="<tr><th>"+data[i].coldDrinksId+"</th><th>"+data[i].coldDrinksName+"</th><th>"+data[i].quantity+"</th><th>"+data[i].unitPrice+"</th><th><Button class='editbtn' id="+data[i].coldDrinksId+"><a href='editform.html?id="+data[i].coldDrinksId+"'>Edit</a></Button>||<Button class='deletebtn' id="+data[i].coldDrinksId+">Delete</Button></th></tr>";
				}
				$("#tbody").html(str);
				
                $(".deletebtn").click(function(e){
                    var id=$(this).attr('id');
                    deleteColdDrinks(id);
                    });
			}
			else
			{
				$("#mike2").html(xmlhttp.status+":"+xmlhttp.statusText);
			}
		}
	});

	}


    
    var deleteColdDrinks=function(id){
		$.ajax({
		url:"http://localhost:5278/api/colddrinks/"+id,
		method:"Delete",
		complete:function(xmlhttp,status){
			if(xmlhttp.status==204 )
			{
				loadList();
                loadtotalprice();
			}
			else
			{
				console.log(xmlhttp.status+":"+xmlhttp.statusText);
			}
		}
	});	
	}

    var addList=function(){
        if($("#coldDrinksName").val()==""||$("#coldDrinksQuantity").val()==""||$("#coldDrinksUnitPrice").val()==""){
            $("#errormsg").html("All Fields Required");
        }
        else{
        console.log("from add list");
        var name=$("#coldDrinksName").val();
        var quantityy=$("#coldDrinksQuantity").val();
        var unitPricee=$("#coldDrinksUnitPrice").val();
		$.ajax({
		url:"http://localhost:5278/api/colddrinks/",
		method:"POST",
		headers:"Content-Type: application/json",
		data:{
			coldDrinksName:name,
            quantity:quantityy,
            unitPrice:unitPricee
		},
		complete:function(xmlhttp,status){
            console.log(xmlhttp);
			if(xmlhttp.status==201)
			{
                $("#coldDrinksName").val("");
                $("#coldDrinksQuantity").val("");
                $("#coldDrinksUnitPrice").val("");
				console.log("success");
				loadList();
                loadtotalprice();
			}
			else
			{
				console.log(xmlhttp.status+":"+xmlhttp.statusText);
			}
		}
	});	
}
	}

    ///load total price
    var loadtotalprice=function(){
		$.ajax({
		url:"http://localhost:5278/api/colddrinks/total-price",
		method:"GET",
		complete:function(xmlhttp,status){
			if(xmlhttp.status==200)
			{
				var data=xmlhttp.responseJSON;
				
				$("#totalprice").html(data);
			}
			else
			{
				$("#mike2").html(xmlhttp.status+":"+xmlhttp.statusText);
			}
		}
	});

	}
    ///addbtn
    $("#addbtn").click(function(e){
        addList();
        });
    loadList(); 
    loadtotalprice();
});