=== VisitPlaceStart ===

{ VisitPlaceQuestState :
    - "RequirementsNotMet": -> requirementsNotMet
    - "CanStart": -> canStart
    - "InProgress": -> inProgress
    - "CanFinish": -> canFinish
    - "Finished": -> finished
    - else: -> lol
}
= requirementsNotMet
your character level is too low 
-> END

= canStart
Can you go to all these places?
* Sure 
    ~ StartQuest("VisitPlaceQuest")
    Thanks, i'll give you the coordinate!
* No, Thanks
    ok, talk to me when you changed your mind.
- -> END

= inProgress
You forget where to go?
-> END

= canFinish
nice
-> END

= finished
Thanks for your help
-> END

= lol
Hello
-> END