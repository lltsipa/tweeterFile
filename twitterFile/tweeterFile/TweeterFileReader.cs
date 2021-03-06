﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tweeterFile
{
    public class TweeterFileReader
    {
        public void getTweeterProfiles(string filePath, Dictionary<string, TweeterUsers> tweeterUsersDictionary)
        {
            using (StreamReader userReader = new StreamReader(@filePath, ASCIIEncoding.Unicode))
            {
                string userfileLine;

                while ((userfileLine = userReader.ReadLine()) != null)
                {
                    TweeterUsers tweeterUsers = null;

                    var userAndFollower = userfileLine.Split(new string[] { "follows" }, StringSplitOptions.None);
                    var checkFollowerExist = tweeterUsersDictionary.ContainsKey(userAndFollower[0].ToString().Trim());

                    if (checkFollowerExist == false)
                    {
                        tweeterUsers = new TweeterUsers();
                        tweeterUsers.Follows = new List<string>();

                        tweeterUsers.Username = userAndFollower[0].ToString().Trim();

                        var getFollowing = userAndFollower[1].ToString().Split(',');

                        foreach (var following in getFollowing)
                        {
                            tweeterUsers.Follows.Add(following.Trim());
                        }

                        tweeterUsersDictionary.Add(tweeterUsers.Username.Trim(), tweeterUsers);
                    }
                    else
                    {
                        var user = tweeterUsersDictionary.FirstOrDefault(findUser => findUser.Key == userAndFollower[0].ToString().Trim());
                        var getFollowing = userAndFollower[1].ToString().Split(',');

                        foreach (var follower in getFollowing)
                        {
                            if (user.Value.Follows.Contains(follower.Trim()) == false)
                            {
                                user.Value.Follows.Add(follower.Trim());
                            }
                        }

                    }

                    var getFollowedUser = userAndFollower[1].ToString().Split(',');

                    foreach (var followed in getFollowedUser)
                    {
                        var findUser = tweeterUsersDictionary.ContainsKey(followed.Trim());
                        if (findUser == false)
                        {
                            tweeterUsers = new TweeterUsers();
                            tweeterUsers.Follows = new List<string>();
                            tweeterUsers.UserFeeds = new List<string>();

                            tweeterUsers.Username = followed.Trim();


                            tweeterUsersDictionary.Add(tweeterUsers.Username.Trim(), tweeterUsers);
                        }
                    }
                    // var checkUserExist 

                }

            }

            using (StreamReader tweetReader = new StreamReader(filePath, ASCIIEncoding.Unicode))
            {
                string tweetfileLine;
                while ((tweetfileLine = tweetReader.ReadLine()) != null)
                {
                    TweeterUsers tweeterUser = new TweeterUsers();
                    tweeterUser.UserFeeds = new List<string>();

                    var userAndPost = tweetfileLine.Split('>');
                    var checkUsers = tweeterUsersDictionary.ContainsKey(userAndPost[0].ToString().Trim());

                    if (checkUsers == true)
                    {
                        tweeterUser = tweeterUsersDictionary.FirstOrDefault(user => user.Key == userAndPost[0].ToString()).Value;

                        var userFollowersList = tweeterUsersDictionary.Where(user => user.Value.Follows.Contains(userAndPost[0].ToString())).ToList();

                        if (userFollowersList.Count > 0)
                        {
                            foreach (var userFollowers in userFollowersList)
                            {
                                if (userFollowers.Value.UserFeeds == null)
                                {
                                    userFollowers.Value.UserFeeds = new List<string>();
                                }
                                userFollowers.Value.UserFeeds.Add("@" + userAndPost[0].ToString() + ": " + userAndPost[1].ToString());
                                // Console.WriteLine(userFollowers.Value);
                            }
                        }

                        if (tweeterUser.UserFeeds == null)
                        {
                            tweeterUser.UserFeeds = new List<string>();
                        }

                        tweeterUser.UserFeeds.Add("@" + userAndPost[0].ToString() + ": " + userAndPost[1].ToString());
                    }

                }
            }
        }

        public void getTweetsByAllUsers(string filePath, Dictionary<string, TweeterUsers> tweeterUsersDictionary)
        {
            //using (StreamReader tweetReader = new StreamReader(filePath, ASCIIEncoding.Unicode))
            //{
            //    string tweetfileLine;
            //    while ((tweetfileLine = tweetReader.ReadLine()) != null)
            //    {
            //        TweeterUsers tweeterUser = new TweeterUsers();
            //        tweeterUser.UserFeeds = new List<string>();

            //        var userAndPost = tweetfileLine.Split('>');
            //        var checkUsers = tweeterUsersDictionary.ContainsKey(userAndPost[0].ToString().Trim());

            //        if (checkUsers == true)
            //        {
            //            tweeterUser = tweeterUsersDictionary.FirstOrDefault(user => user.Key == userAndPost[0].ToString()).Value;

            //            var userFollowersList = tweeterUsersDictionary.Where(user => user.Value.Follows.Contains(userAndPost[0].ToString())).ToList();

            //            if (userFollowersList.Count > 0)
            //            {
            //                foreach (var userFollowers in userFollowersList)
            //                {
            //                    if (userFollowers.Value.UserFeeds == null)
            //                    {
            //                        userFollowers.Value.UserFeeds = new List<string>();
            //                    }
            //                    userFollowers.Value.UserFeeds.Add("@" + userAndPost[0].ToString() + ": " + userAndPost[1].ToString());
            //                    // Console.WriteLine(userFollowers.Value);
            //                }
            //            }

            //            if (tweeterUser.UserFeeds == null)
            //            {
            //                tweeterUser.UserFeeds = new List<string>();
            //            }

            //            tweeterUser.UserFeeds.Add("@" + userAndPost[0].ToString() + ": " + userAndPost[1].ToString());
            //        }

            //    }
            //}
        }
    }
}
