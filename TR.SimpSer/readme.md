# 実装の解説
## 概説
今回はファイル名から設定を読み出し、あらかじめ設定をすることで、楽な実装を実現しています。  
まぁ本当はDataNameではなくDataNumでの設定をさせたかったのですが、あまりにも不親切だと思ったため、名前での設定もできるようにしています。  
数字で設定した方がロード時間は短くなると思います。たぶん。

実装について、System.IO.PortsのSerialPortクラスを用いてシリアル通信を行っています。自分で設定しているのはBaudRateとPortNameのみで、それ以外はデフォルトの設定を使用しています。  
はじめに設定値の有効性やNULLチェックなどを行った後に、DataNameおよびDataNumの検証に入ります。  
DataNumの検証はint.Parseを用いて行い、エラーを吐きやがったらそれをcatchしてDataNameの検証に入ります。  
DataNameの検証ではswitch-caseを用いてstringの比較を行い、一致した場合は対応するDataNumを設定します。DataNameがcaseに見当たらなかった場合はエラーを吐きます。  
DataNumが設定されると、それが0でない限り、シリアル通信が開始されます。DataNumで無効な値が設定された場合も通信が開始されるため、注意してください。  
ほんとはイベントドリブンにした方が速いんでしょうけど、今回は地道にifとswitch-caseを用いて送信のTFを判断しています。  
PI終了時は自動でポートが閉じられます。強制終了したときの挙動は知りません。

一応DataNumの値の範囲によって実行場所を特定できるようにしています。  
DataNumの値の範囲と実行場所の対応は以下の表を参考にしてください。
|from|to|Method|Remarks|
|----|----|----|----|
|0|0|(None)|処理は実行されません。|
|1|24|SetVehicleSpec|Spec情報群|
|25|25|Initialize|起動時ブレーキ位置情報|
|51|51|SetPower|力行ハンドル位置|
|61|61|SetBrake|制動ハンドル位置|
|71|71|SetReverser|レバーサーハンドル位置|
|81|81|DoorOpen/Close|ドア情報|
|91|91|SetSignal|信号現示番号|
|100|355|SetBeacon|Beacon情報(StrArrのみ提供)|
|500|520|KeyDown/Up|キー押下情報|
|901|999|Elapse|State情報群|
|1000|1255|Elapse|Panel状態|
|2000|2155|Elapse|Sound状態|

## DataNumについて
DataNumは、DataNameと対応した数字です。以下に対応表を置いておきます。
|Num|Name|Remarks|
|----|----|----|
|0|(string.Empty)|シリアル通信は行われません。  
但し、他の変数の有効性チェックは行われます。|
|901|STTb|ByteArray|
|903|SPDf|現在速度(float)|
