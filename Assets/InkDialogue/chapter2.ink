=== LelakiTuaMulai ===
Tuan, sepertinya seseorang sedang dalam kesulitan. Mari Kita tolong. #speaker:Dora #portrait:dora
- ->END

== LelakiTuaMemintaTolong ===
Tolong... Tolong... #speaker:Lelaki Tua #portrait:npc

- ->END

=== LelakiTua ===
Tuan, apakah kamu baik-baik saja? Bagaimana dirimu bisa ada di tengah hutan sendirian #speaker:Aji Saka #portrait:ajisaka
Aku berasal dari Kerajaan Medang Kamulan... Aku dan warga lainnya kabur karena Prabu Dewata Cengkar, Raja kami. #speaker:Lelaki Tua #portrait:npc
Ia kejam... ia meminta tumbal manusia untuk disantap. Tolong kami tuan, kami sudah lama diterror oleh Dirinya. 
Apa? Seorang raja... memakan manusia? Sungguh jahat. #speaker:dora #portrait:dora
*[Menolak Membantu]
    Maaf... tapi itu bukan kewajibanku. Engkau lebih baik mencari orang lain yang bisa menolong. #speaker:Aji Saka #portrait:ajisaka
    Tuan, bukankah kita harus menolong siapa pun yang membutuhkan? #speaker:dora #portrait:dora
    Jika kita berpaling, berapa banyak orang yang akan menjadi korban?
    Baiklah... aku akan membantumu. Tunjukkan jalan, dan aku akan menghadapi Prabu Dewata Cengkar. #speaker:Aji Saka #portrait:ajisaka
    ->LelakiTuaTerimakasih
*[Menerima Permintaan tolong]
    Jika memang demikian, aku tak bisa tinggal diam. Menolong rakyat yang menderita adalah kewajiban seorang ksatria. #speaker:Aji Saka #portrait:ajisaka
    Aku akan melakukan sebisanya untuk menghentikan kebiadaban itu.
    ->LelakiTuaTerimakasih

=== LelakiTuaTerimakasih ===
Terima kasih banyak Tuan! Jika begitu, pergilah ke desa terdekat. Cari seorang wanita bernama NYAI SENGKERAN. Dia akan membantumupergi menuju kerajaan. #speaker:Lelaki Tua #portrait:npc
- ->END

=== NyaiSengkeran ===

{ LawanPerampokMenujuDesaState :
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
Tuan muda! Tolonglah! Jalan menuju desa dikuasai gerombolan perampok. Mereka merampas dan menakuti orang-orang. #speaker:Nyai Sengkeran #portrait: npc
Tenanglah. Aku akan menyingkirkan mereka. Tidak seorang pun berhak menindas orang lain. #speaker:Aji Saka #portrait:ajisaka 
~ StartQuest("LawanPerampokMenujuDesa")
- -> END

= inProgress
Tuan muda! Tolonglah! #speaker:Nyai Sengkeran #portrait: npc
-> END

= canFinish
 ~ FinishQuest("LawanPerampokMenujuDesa")
Terima kasih, tuan muda. Berkatmu jalan kembali aman. #speaker:Nyai Sengkeran #portrait: npc
Tapi... sebelumnya, aku belum pernah melihat wajah kalian di sekitar sini. Kalian tampak asing.
Apakah ada yang bisa kubantu, atau sebenarnya apa tujuan kalian datang ke wilayah Medang Kamulan?

Aku adalah Aji Saka, dan ini abdi setiaku, Dora. Kami mendapat arahan dari seorang lelaki tua yang melarikan diri dari kerajaan. #speaker:Aji Saka #portrait:ajisaka 
Ia memintaku untuk mencari seorang wanita bernama Nyai Sengkeran yang dapat menolong kami menuju Medang Kamulan.

Jika begitu... rupanya benar kata orang itu. Aku memang Nyai Sengkeran. Kalian datang di saat yang tepat. #speaker:Nyai Sengkeran #portrait: npc
Ikutlah bersamaku, aku akan membawamu ke desa. Dari sana, kita bisa merencanakan langkah menuju istana.
 ~ PlayCutscene("CutsceneNyaiSengkaren")
- -> END

= finished
-> END

- -> END
