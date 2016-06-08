$(function () {
    //隐藏显示图片的表格
    $('#tbl').hide();
   
    $('#testId').JSAjaxFileUploader({
        uploadUrl: '/Goods/UploadFile',
        inputText: '请上传物品图片',
        //fileName: 'photo',
        maxFileSize: 5242880,    //Max 500 KB file 1kb=1024字节
        allowExt: 'gif|jpg|jpeg|png',
        zoomPreview: false,
        zoomWidth: 360,
        zoomHeight: 360,
        beforesend: function (file) {
            if ($('.imgName').text() != "") {
                deleteImg();
                $('#tbl').hide();
            }
        },
        success: function (data) {
            createTableTr();
            $('#tbl').show();
         
            $('.showImg').attr("src", data.path);
            $('.imgName').text(data.name);
        },
        error: function (data) {
            alert("出错了");
        }
    });

    //点击删除链接删除刚上传图片
    $('#tbl').on("click", ".delImg", function () {
        deleteImg();
        //window.location.reload();
    });
});

//删除图片方法：点击删除链接或上传新图片删除原先图片用到
function deleteImg() {
    $.ajax({
        cache: false,
        url: '/Goods/DeleteFileByName/',
        type: "POST",
        data: { name: $('.imgName').text() },
        success: function (data) {
            if (data.msg) {
                //alert("图片删除成功");
                $('.delImg').parent().parent().remove();

            }
        },
        error: function (jqXhr, textStatus, errorThrown) {
            alert("出错了 '" + jqXhr.status + "' (状态: '" + textStatus + "', 错误为: '" + errorThrown + "')");
        }
    });
}

//创建表格
function createTableTr() {
    var table = $('#tbl');
    table.append("<tr><td><img class='showImg'/></td><td colspan='2'><span class='imgName'></span></td><td><a class='delImg' href='javascript:void(0)'>删除</a></td></tr>");
}