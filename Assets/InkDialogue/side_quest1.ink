=== MissingBrotherStart ===

{ MissingBrotherState :
    - "RequirementsNotMet": -> requirementsNotMet
    - "CanStart": -> canStart
    - "InProgress": -> inProgress
    - "CanFinish": -> canFinish
    - "Finished": -> finished
    - else: -> END
}
= requirementsNotMet
-> END

= canStart
Hah... terima kasih tuan, kalau kau tak datang tepat waktu, mungkin diriku telah dihajar habis-habisan #speaker: Jaka
Sama sama tuan. Apakah kau baik-baik saja? Apa yang sebenarnya terjadi? #speaker: Aji Saka #portrait:ajisaka
Jadi begini tuan, pertama-tama nama Saya Jaka. #speaker: Jaka #portrait:jaka
Aku dan saudaraku, Raka, sedang mencari kayu bakar dan juga tanaman herbal. 
Tapi tiba-tiba kami disergap oleh penjahat. 
Kami pun mencoba untuk kabur, tapi kami terpisah... 
aku mencoba mengecoh mereka tapi akhirnya malah diriku yang terkepung
Jadi Raka masih tersesat dan dikepung oleh penjahat tadi? #speaker: Aji Saka #portrait:ajisaka
Ya tuan... aku khawatir dia dalam bahaya, bisakah anda mencari saudara saya? #speaker: Jaka #portrait:jaka
Hanya dia keluarga yang saat ini saya punya 
~ StartQuest("MissingBrother")
* [Aku akan bantu kalian?]
    Tentu saja, saya akan membantu mu Jaka. tetaplah di sini dan bersembunyi. #speaker: Aji Saka
    Saya akan memberitahu Raka kalau dirimu menunggu di sini. 
    Terima kasih banyak tuan yang baik hati, saya akan menunggu di sini dan bersembunyi #speaker: Jaka
* [Apa yang akan kalian berikan padaku?] 
    Semua ada harganya, jika aku menolong nya, apa yang kalian bisa berikan padaku? #speaker: Aji Saka
    Misi pencarian dan penyelamatan itu tidak murah 
    Tolong tuan, saya mungkin hanya seorang yang jelata. Tapi saat ini semua barang bawaan kami ada di Raka. #speaker: Jaka
- -> END

= inProgress
You forget where to go?
-> END

= canFinish
  Oh terima kasih banyak tuan. Orang-orang ini telah mengganggu saya dan saudara saya saat kami sedang mencari kayu bakar. #speaker: Raka
Mereka memalak dan juga mengancam akan menghajar kami. Kami melarikan diri, tapi sayangnya kami terpisah... 
Aku harap saudara ku baik-baik saja...
Sama sama, senang rasanya bisa membantu. Perkenalkan saya Aji Saka. Apakah namamu Raka? 
Jaka meminta diriku untuk mencarimu. Dia menunggu mu di dekat jalan keluar hutan. #speaker: Aji Saka
Iya benar, nama Saya Raka. Syukurlah, berarti Jaka dalam kondisi aman. Terima kasih Tuan. 
Sebelum saya pergi, saya hanya bisa memberi segini saja untuk membayar Tuan karena telah membantu kami. #speaker: Raka
* [Aku akan membantu kalian secara sukarela]
    Tidak perlu repot-repot Raka, saya membantu kalian secara sukarela. Kalian hanya memiliki satu sama lain. #speaker: Aji Saka
    Terima kasih banyak tuan, semoga perjalanan anda senantiasa diberkati. #speaker: Raka
    Hati-hati juga karena konon katanya ada monster mengerikan yang berkeliaran dan menjaga sebuah tempat di daerah hutan ini. 
* [Apa yang bisa kalian bayar?]
    Hanya uang dan potion kecil untuk usaha ku ini? #speaker: Aji Saka
    Dirimu kira gampang, mencari dan menyelamatkan dirimu dari penjahat-penjahat ini? 
    Mohon maaf tuan, tapi hanya ini saja yang kami punya... sisanya hanya lah kayu bakar dan juga buah-buahan untuk bekal kami. #speaker: Raka
    Seandainya kami punya lebih, kami pun akan memberikan Anda lebih Tuan. 
- -> END

= finished
Thanks for your help
-> END

- -> END