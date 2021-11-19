

function updateCollapsible(){
    $("[data-toggle='open-collapsible']").each(function(){
        const button=this;
        //we need this check, otherwise the dropdown buttons will reset to "expand photo" even if they are expanded
        if(!$(button).css("background").includes("up-chevron"))$(button).css("background","url('https://localhost:5001/images/down-chevron.png') no-repeat");
        $(button).css("background-size", "contain");
        $(button).css("background-repeat","no-repeat");
        $(this).click(function(){
            this.classList.toggle("active");
        })
    })
}

function ShowOrHide(linkID){
    const button = $("[data-toggle='open-collapsible'][data-link='"+linkID+"']");
    const content = $("[data-toggle='collapsible'][data-link='"+linkID+"']");
    if ($(content).css("display") === "block") {
        $(content).fadeOut();
        $(button).css("background","url('https://localhost:5001/images/down-chevron.png')");
        $(button).css("background-size", "contain");
        $(button).css("background-repeat","no-repeat");
    } else {
        //we don't want to reload data if the dropdown is already filled
        if($(content).html()==="") loadData(linkID);
        else{
            //if we already the collapsible loaded just hide it
            $(content).delay(500).fadeIn();
            $(button).css("background","url('https://localhost:5001/images/up-chevron.png') no-repeat");
            $(button).css("background-size", "contain");
            $(button).css("background-repeat","no-repeat");
        }
    }
}

function loadData(uid){
    let url=$("#retrieveType").prop("checked")?"/Home/Leaves/?uid=":"/Home?uid=";
    $.ajax({
        type:"GET",
        url:url+uid,
        success:(data)=>{
            //load data through html
            if(!$("#retrieveType").prop("checked")) $("[data-toggle='collapsible'][data-link='"+uid+"']").html(data);
            else{
                for(element of Object.values(data)){
                    //we need to load from json
                    //create the template
                    if(element.Children && element.Children.length>0){
                        $("[data-toggle='collapsible'][data-link='"+uid+"']").html(` 
                        <div class="collapsible">
                             <span>${element.Name}</span>
                             <span>${element.AssignedTo}</span>
                             <span type="button" class="open-collapsible" data-toggle="open-collapsible" data-link="${element.UID}" onclick="ShowOrHide('${element.UID}')"></span>
                             <div class="content" data-toggle="collapsible" data-link="${element.UID}"></div>
                        </div>
                    `)
                    }else{
                        //last leaf
                        $("[data-toggle='collapsible'][data-link='"+uid+"']").html(` 
                        <div class="collapsible">
                            <span>${element.Name}</span>
                             <span>${element.AssignedTo}</span>
                        </div>
                    `)
                    }
                }
            }
            updateCollapsible();
            $("[data-toggle='collapsible'][data-link='"+uid+"']").fadeIn();
            $("[data-toggle='open-collapsible'][data-link='"+uid+"']").css("background","url('https://localhost:5001/images/up-chevron.png') no-repeat");
            $("[data-toggle='open-collapsible'][data-link='"+uid+"']").css("background-size", "contain");
            $("[data-toggle='open-collapsible'][data-link='"+uid+"']").css("background-repeat","no-repeat");
        },
    }).always(()=>{$(".loader").css("display","none");})
        .fail(()=>{$("#modalMessage").text("error while retrieving data from the server");$("#modal").css("display","block");});
    $(".loader").css("display","block");
    
}