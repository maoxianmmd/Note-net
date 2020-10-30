/**生成随机的字符/字符串*/
function randomword(len) {
    // 生成一个或多个字符组成的字符串
    len = len || 1;
    var ss = 'ABCDEFGHJKMNPQRSTWXYZabcdefhijkmnprstwxyz2345678';    /****默认去掉了容易混淆的字符oOLl,9gq,Vv,Uu,I1****/
    var ssl = ss.length;
    var returN = "";/**返回的字符/字符串*/
    var limt = ss.split("");
    for (var i = 0; i < len; i++) {
        returN += limt[Math.floor(Math.random() * ssl)];
    }
    return returN;
}

/**该程序的验证码图片分为两个部分：背景图片部分和文字部分
           -每次生成验证码时，都需要将验证码图片重置（最简单方法是重置画布宽高）、
           -程序执行顺序
               -1.第一次自动生成一个验证码，验证码用正则表达式保存
                2.若点击图片，将会重置图片，并重置验证码的正则表达式
                3.若点击确定，则提交输入框结果，此时进入判断程序，检查输入是否符合正则表达式规则*/


/**定义随机生成的文字*/
var txt = "";
var oldtxtword = "";/**记录每次生成的每个字符*/

/** 主函数*/
function yanzhengma(btn, yzm1, fun0, fun1, fun2) {
    var btn = document.getElementById(btn);
    var txtt = yzmtp(yzm1);//生成验证码，返回正则表达式字符串
    var txtx = new RegExp(txtt, "i");//将字符串转换为正则表达式
    btn.onclick = function () {
        /**重置正则表达式*/
        // 当点击图片后，正则表达式并未被重置，因此要重新检查赋值
        txtt = yzmtp(yzm1);
        txtx.compile(txtt, "i");
        var yzm = fun0();
        //console.log(txtx);
        //console.log(yzm)
        //console.log(txtx.test(yzm))
        if (txtx.test(yzm)) {
            console.log(1);
            fun1();
        } else {
            fun2();
        }
    };
    return txtx;
}
/**生产验证码图片*/
function yzmtp(yzm1) {
    var yy = document.getElementById(yzm1);
    var yzmwz = yy.getContext("2d");/*验证码文字部分*/
    var yzmbj = yy.getContext("2d");/*验证码背景部分*/
    var yzmbjbk = yzmbj.createLinearGradient(0, 0, 100, 40);/*验证码背景边框*/

    /**第一次生成验证码*/
    if (txt == "") {
        // 验证码背景部分动画
        backgroun(yzmbj, yzmbjbk, true);
        reword(yzmwz);
    }

    /**将txt中内容进行不区分大小写的正则转换*/
    var txtx = zzbds(txt);

    /**点击图片后重新生成验证码*/
    yy.onclick = function () {
        // 清除画布上原来的内容
        backgroun(yzmbj, yzmbjbk, false);
        reword(yzmwz);
        txtx = zzbds(txt);
        return txtx;
    }
    return txtx;
}

/**点击图片后重新生成验证码*/
function reword(yzmwz) {
    var xx = Math.floor(Math.random() * 10), yy = Math.floor(Math.random() * 10);/*验证码位置*/
    for (var i = 0; i < 4; i++) {
        var txtword = randomword();/*定义生成验证码的单个字母*/
        var bh = 20 + Math.floor(Math.random() * 15) + "px";/*生成的字体大小*/
        // console.log(bh+"  "+typeof bh);
        oldtxtword = txtword;
        txt += txtword;/*四次循环后获得完整的验证码*/
        if (txt.length > 4) {/**重置图片，txt先清空再重新赋值*/
            txt = oldtxtword;
        }
        // console.log(txt);//检查验证字
        //文字部分
        yzmwz.textBaseline = "top";
        yzmwz.font = bh + " italic"
        yzmwz.fillStyle = "black";
        yzmwz.fillText(txtword, xx, yy);
        xx += (15 + Math.floor(Math.random() * 5));//用于布局文字位置
        yy += Math.floor(Math.random() * 5);
        if (yy >= 15) {//防止文字超越可视界面
            yy = yy - Math.floor(Math.random() * 10);
        }
    }
}

/**验证码背景*/
function backgroun(yzmbj, yzmbjbk, ss) {
    if (ss) {


        /**每当画布的宽，高重设时，画布内容就会被清空*/
        yzmbj.height = yzmbj.height;

    }
    yzmbjbk.addColorStop(0, "yellow");
    yzmbjbk.addColorStop(0.25, "red");
    yzmbjbk.addColorStop(0.5, "#9674ff");
    yzmbjbk.addColorStop(0.75, "green");
    yzmbjbk.addColorStop(1, "blue");
    yzmbj.fillStyle = yzmbjbk;
    yzmbj.fillRect(0, 0, 100, 40);
}

/**转换为正则表达式*/
function zzbds(words) {
    var ss = words.split("");
    var rewords = "^";/*返回值*/
    for (var i = 0; i < ss.length; i++) {
        rewords += ss[i];
    }
    rewords += "$";
    return rewords;//返回String类型
}