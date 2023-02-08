using System;
using Babaturan.Helpers;

namespace Babaturan.Data
{
    public class AppState
    {

        public event Action<string> OnProfileChange;
        public event Action<string> OnEventChange;
        public event Action<string> OnPostChange;
        public event Action<string> OnStoryChange;
        public event Action<string> OnNewPostChange;
        public event Action<string, GeoLocation> OnLocationChange;


        public void RefreshProfile(string username)
        {
            ProfileStateChanged(username);
        }

        public void RefreshEvent(string username)
        {
            EventStateChanged(username);
        }
        public void RefreshPost(string username)
        {
            PostStateChanged(username);
        }
        public void RefreshStory(string UID)
        {
            StoryStateChanged(UID);
        }
        public void CreateNewPost(string username)
        {
            NewPostStateChanged(username);
        }

        public void SelectLocation(string username, GeoLocation loc)
        {
            LocationStateChanged(username, loc);
        }

        private void NewPostStateChanged(string username) => OnNewPostChange?.Invoke(username);
        private void StoryStateChanged(string UID) => OnStoryChange?.Invoke(UID);
        private void PostStateChanged(string username) => OnPostChange?.Invoke(username);
        private void EventStateChanged(string username) => OnEventChange?.Invoke(username);
        private void ProfileStateChanged(string username) => OnProfileChange?.Invoke(username);

        private void LocationStateChanged(string username, GeoLocation loc) => OnLocationChange?.Invoke(username, loc);

    }
}

