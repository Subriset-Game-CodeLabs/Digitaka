=== MerchantChat ===
Halo, mau beli sesuatu? #speaker:Penjual #portrait:npc
* [Buy Item]
    ~ OpenShop() 
    Silahkan untuk melihat-lihat #pause:pause
    -> Leave
* [Leave]
    -> Leave
* [Tambah Morale]
    morale kamu bertambah 
    ~ChangeMorale(20)
    -> Leave
* [Kurang Morale]
    morale kamu berkurang 
    ~ChangeMorale(-20)
    -> Leave
=== Leave ===
Kembali lagi nanti.
-> END