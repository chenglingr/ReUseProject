function Delete1(ArticleID) {
    if (confirm("确定要删除？")) {
        url = "/Goods/Delete";
        parameter = { id: ArticleID };
        $.post(url, parameter, function (data) {
            alert("删除成功！");
            window.location = "/Goods/MyIndexList";
        });
    }
}

$(document).ready(function () {
    var State;
    var GoodsID;
    $('#EditState').on('show.bs.modal', function (event) {
        var button = $(event.relatedTarget) // Button that triggered the modal
        GoodsID = button.data('whatever') // Extract info from data-* attributes
        // If necessary, you could initiate an AJAX request here (and then do the updating in a callback).
        // Update the modal's content. We'll use jQuery here, but you could use a data binding library or other methods instead.
        State = button.data('state')
        var modal = $(this)
        //modal.find('#btnok').text(GoodsID+" "+state)
        if (State == 0) {
            $("#btnwait").removeClass().addClass("btn btn-success");
            $("#btnok").removeClass().addClass("btn btn-default");
            $("#btncancel").removeClass().addClass("btn btn-default");
        }
        else if (State == 1) {
            $("#btnwait").removeClass().addClass("btn btn-default");
            $("#btnok").removeClass().addClass("btn btn-success");
            $("#btncancel").removeClass().addClass("btn btn-default");
        }
        else
        {
            $("#btnwait").removeClass().addClass("btn btn-default");
            $("#btnok").removeClass().addClass("btn btn-default");
            $("#btncancel").removeClass().addClass("btn btn-success");
        }
    })
    $('#ShowTran').on('show.bs.modal', function (event) {
        var button = $(event.relatedTarget) // Button that triggered the modal
        GoodsID = button.data('whatever') // Extract info from data-* attributes
     
        var modal = $(this)
        modal.find('#Trans').html(GoodsID);
        url = "/Tran/List";
        parameter = { id: GoodsID };
        $.post(url, parameter, function (data) {
          
            var msg = "";
        
            for (var i = 0, length = data.Data.length; i < length; i++) 
            {
                var ta = "<span class=\"label label-success\">";
                var xy = 0;
                if (data.Data[i].qq != "") { xy = 1; ta = ta+"QQ:" + data.Data[i].qq + "&nbsp;&nbsp;"; }
                if (data.Data[i].email != "") { xy = 1; ta = ta + "邮箱：" + data.Data[i].email + "&nbsp;&nbsp;"; }
                if (data.Data[i].weixin != "") { xy = 1; ta = ta + "微信：" + data.Data[i].weixin + "&nbsp;&nbsp;"; }
                if (data.Data[i].tel != "") { xy = 1; ta = ta + "电话：" + data.Data[i].tel; }
                if (xy ==0) { ta = ta + "暂无发信人[" + data.Data[i].name + "]联系方式，请看留言"; }
                ta=ta+"</span>"
                var da = new Date(parseInt(data.Data[i].createdate.substr(6)));
                var da1 = da.getFullYear() + "-" + da.getMonth() + "-" + da.getDate();             
                msg += da1 + " [" + data.Data[i].name + "]发来信息:<br/>" + "<DiV class=\"alert alert-warning\">" + ta + "<br/><br/><span class=\"label label-info\">内容:" + data.Data[i].content + "</span></div>";
           }
            $('#Trans').html(msg);
        });
   
    })
    $("#btnwait").click(function () {
        $("#btnwait").removeClass().addClass("btn btn-success");
        $("#btnok").removeClass().addClass("btn btn-default");
        $("#btncancel").removeClass().addClass("btn btn-default");
        State = 0;
    });
    $("#btnok").click(function () {
        $("#btnwait").removeClass().addClass("btn btn-default");
        $("#btnok").removeClass().addClass("btn btn-success");
        $("#btncancel").removeClass().addClass("btn btn-default");
        State = 1;
    });
    $("#btncancel").click(function () {
        $("#btnwait").removeClass().addClass("btn btn-default");
        $("#btnok").removeClass().addClass("btn btn-default");
        $("#btncancel").removeClass().addClass("btn btn-success");
        State = 2;
    });
    $("#btnsubmit").click(function () {
        url = "/Goods/EditState";
        parameter = { id: GoodsID ,state:State};
            $.post(url, parameter, function (data) {
                alert("更新成功！"+data);
                window.location = "/Goods/MyIndexList";
            });

    });
       
});