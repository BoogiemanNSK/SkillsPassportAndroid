using UnityEngine;
using UnityEngine.UI;

namespace IntroScripts
{
    public class IntroQuizResults : MonoBehaviour {

        public Text FinalText;
        public Transform SkillsGrid;
        public SkillPrefabGenerator SkillDescExample;
    
        public void OnEnter(DataBaseManager.UserData userData, bool canPass) {
            FinalText.text = canPass ? Constants.IntroFinalStringReady : Constants.IntroFinalStringNotReady;

            for (var i = 0; i < userData.Skills.Length; i++) {
                var createdSkill = Instantiate(SkillDescExample, SkillsGrid);
                var charIndexWithoutGender = userData.CharIndex % (Constants.CharactersImageLink.Length / 2);
            
                createdSkill.FillInactive(
                    i,
                    userData.Skills[i],
                    Constants.RequiredSkillsToPass[charIndexWithoutGender][i],
                    Constants.SkillsImageLink[i],
                    Constants.SkillsNames[i]
                );
            }
        }
    
    }
}