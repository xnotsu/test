$(function() {
    $(document).keydown(function(event){
        // クリックされたキーコードを取得する
        var keyCode = event.keyCode;
        // F5 の場合は falseをリターン
        if(keyCode == 116) {
            //console.log("F5");
            return false;
        }
        // バックスペースキーを制御する
        if(keyCode == 8){
            //console.log("Backspace");
            return false;
        }
        // Alt キーが押されているかを取得する
        var altKey = event.altKey;

        if (altKey) {
            if (keyCode == 37 || keyCode == 39) {
                //console.log("[Alt + →] または [Alt + ←]");
                return false;
            }
        }
    })
});

// History API が使えるブラウザかどうかをチェック
if (window.history && window.history.pushState) {
    //. ブラウザ履歴に１つ追加
    history.pushState("nohb", null, "");
    $(window).on("popstate", function (event) {
        //. このページで「戻る」を実行
        if (!event.originalEvent.state) {
            //. もう一度履歴を操作して終了
            history.pushState("nohb", null, "");
            return;
        }
    });
}

window.onbeforeunload = function(e) {
    e.returnValue = "本当にページを閉じますか？";
}
