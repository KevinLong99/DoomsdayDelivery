using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

namespace OpenAI
{
    public class ChatGPT : MonoBehaviour
    {
        [SerializeField] private ScrollRect scroll;
        [SerializeField] private RectTransform sent;
        [SerializeField] private RectTransform received;

        public Typewriter_UI typewriter;

        private float height;
        private OpenAIApi openai = new OpenAIApi("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");

        private List<ChatMessage> messages = new List<ChatMessage>();
        private string prompt = "Give instructions in a paragraph, not a list, on how to make a pizza in different styles.";

        private void AppendMessage(ChatMessage message)
        {
            scroll.content.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 0);

            var item = Instantiate(message.Role == "user" ? sent : received, scroll.content);
            item.GetChild(0).GetChild(0).GetComponent<Text>().text = message.Content;
            item.anchoredPosition = new Vector2(0, -height);
            LayoutRebuilder.ForceRebuildLayoutImmediate(item);
            height += item.sizeDelta.y;
            scroll.content.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height);
            scroll.verticalNormalizedPosition = 0;
        }

        public async void SendReply()
        {
            string alwaysAnswer = "How do I make a pizza?";

            var newMessage = new ChatMessage()
            {
                Role = "user",
                //Content = alwaysAnswer
            };
            
            //AppendMessage(newMessage);

            newMessage.Content = prompt + "\n" + alwaysAnswer;

            messages.Add(newMessage);

            
            // Complete the instruction
            var completionResponse = await openai.CreateChatCompletion(new CreateChatCompletionRequest()
            {
                Model = "gpt-3.5-turbo-0613",
                Messages = messages
            });

            if (completionResponse.Choices != null && completionResponse.Choices.Count > 0)
            {
                var message = completionResponse.Choices[0].Message;
                message.Content = message.Content.Trim();
                
                messages.Add(message);
                AppendMessage(message);

                string aiResponse = message.Content;
                //Debug.Log(aiResponse);

                typewriter.StartTypewriterView(aiResponse);

            }
            else
            {
                Debug.LogWarning("No text was generated from this prompt.");
            }
        }
    }
}
