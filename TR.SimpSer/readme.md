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
|902|SPDi|現在速度(int)|
|903|SPDf|現在速度(float)|
|904|LOCd|現在位置(double)|
|905|TMEi|現在時刻(ms 整数)|
|906|TMEt|現在時刻(時刻標準形式 HH:MM:SS.MS)|
|907|BCPi|BC圧(整数)|
|908|BCPf|BC圧(float)|
|909|MRPi|MR圧(整数)|
|910|MRPf|MR圧(float)|
|911|ERPi|ER圧(int)|
|912|ERPf|ER Pressure(float)|
|913|BPPi|BP Pressure(int)|
|914|BPPf|BP Pressure(float)|
|915|SAPi|SAP Pressure(int)|
|916|SAPf|SAP Pressure(float)|
|917|CURi|Current(int)|
|918|CURf|Current(float)|
なお、整数(int)は整数値で、floatは小数点以下第4位まで、doubleは小数点以下第6位まで送信します。
