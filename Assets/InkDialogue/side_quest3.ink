=== KillEnemyStart ===

{ KillEnemyState :
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
Tolong! Ada orang yang menghadangku di depan, aku tidak bisa lewat. Bisakah kamu menyingkirkan mereka? #speaker: Jaka #portrait:jaka 

* [Mau Bantu]
    ~ ChangeMorale(20)
    Baik, aku akan menyingkirkan mereka untukmu. #speaker: Aji Saka #portrait:ajisaka
    Terima kasih! Aku tahu aku bisa mengandalkanmu. Hati-hati, mereka berbahaya. #speaker: Jaka  #portrait:jaka
    Tenang saja, aku akan mengurus mereka. #speaker: Aji Saka #portrait:ajisaka
    ~ StartQuest("KillEnemy")
* [Tidak Mau Bantu]
    ~ ChangeMorale(-20)
    Apa? Kamu serius?! Kalau begitu aku tidak bisa lewat… tapi kalau aku tertahan di sini, kita berdua juga tidak bisa lanjut. Jadi suka tidak suka, kamu tetap harus menyingkirkan mereka. #speaker: Jaka #portrait:jaka
    Hhh… baiklah, sepertinya aku tidak punya pilihan. #speaker: Aji Saka #portrait:ajisaka
    ~ StartQuest("KillEnemy")
- -> END

= inProgress
Kamu harus menyingkirkan mereka! Aku tidak bisa lewat! #speaker: Jaka #portrait:jaka
-> END

= canFinish
  Kamu berhasil! Sekarang jalannya aman. Terima kasih, ini koin untukmu. #speaker: Jaka #portrait:jaka
  Sudahlah, yang penting kita bisa lanjut sekarang. #speaker: Aji Saka #portrait:ajisaka
  ~ FinishQuest("KillEnemy")
- -> END

= finished
Terima kasih sudah membantuku. Semoga kebaikanmu dibalas. #speaker: Jaka #portrait:jaka
-> END

- -> END