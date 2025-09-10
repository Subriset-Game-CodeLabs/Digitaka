// External Functions
EXTERNAL StartQuest(questId)
EXTERNAL AdvanceQuest(questId)
EXTERNAL FinishQuest(questId)
EXTERNAL OpenShop()
EXTERNAL ChangeMorale(amount)

// quest ids (questid + "Id" for variable name)
VAR VisitPlaceQuestId = "VisitPlaceQuest"
VAR MissingBrotherId = "MissingBrother"
VAR SideQuest2Id = "SideQuest2"
VAR KillEnemyId = "KillEnemy"

// quest states (questid + "State" for variable name)
VAR VisitPlaceQuestState = "REQUIREMENTS_NOT_MET"
VAR MissingBrotherState = "REQUIREMENTS_NOT_MET"
VAR KillEnemyState = "REQUIREMENTS_NOT_MET"

INCLUDE visit_place_quest_npc.ink
INCLUDE merchant_npt.ink

// side quest
INCLUDE side_quest1.ink
INCLUDE side_quest2.ink
INCLUDE side_quest3.ink

// Main Story
INCLUDE chapter1.ink


=== testspeaker ===
Hi my name is aji saka #speaker:Aji Saka #portrait:ajisaka
I'm a traveler from a far 
What happend to this village?
My village is being controlled by the evil king #speaker:NPC  #portrait:npc
he demand sacrifice everyday 
WHAT!!! #speaker:Aji Saka #portrait:ajisaka

- -> END