# Social Content Sharing Platform

Open source, community driven, audience-centric platform to share posts
and communicate with peers on the internet without the fear of the 'algorithm'.

SCSP is a platform that lets user share their posts and follow other users.
Users can like,dislike,comment on users' posts. Users recieve notification
on comments on their posts. User can upload images to be posted inside the
post. They can also upload image as their display picture, without which a
default display picture is assigned which is made from their name provided
by taking the initials.

Users can also message other users and the messages are stored in the data
base as plain text (no encryption). The user credentials are not stored in
plain text, they are salted and hashed before storing in the database.

User auth is performed using cookies on the server side. Multiple users can
use the platform simultaneously.

The platform is mobile friendly and primarily built using bootstrap, along
with some custom styles.

The posts on main page are ranked using hot-ranking algorithm so newer posts
are shown at top. Posts with more like:dislike are shown above others. 

![image of hot rank algorithm in latex](https://miro.medium.com/max/640/0*21Ezm5SbYie_a3oD.png)

The comments on each posts are sorted only on merit and not age. 
The comments are ranked using the Wilson Score Confidence Sort Algorithm 
to predict the merit of a comment from the number of likes and dislikes 
available at the time. The algorithm gets better with more data.

![image of confidence sort algorithm in latex](https://miro.medium.com/max/634/0*fYYMk52egqm-9h55.png)

The formula used is talked in detail, along with a python implementation 
in this medium article by [Amir Salihefendic](https://medium.com/@amix3k): 
[How Reddit ranking algorithms work](https://medium.com/hacking-and-gonzo/how-reddit-ranking-algorithms-work-ef111e33d0d9)

The intention of making this platform was to have a platform that respects
the user's choices of what they want to see by implementing a community
logic of post ranking, instead of an advertiser centric one. Although media
like reddit and 4chan already do this, they primarily focus on communities as
a whole, and people follow subreddits, instead of individual users. Furthermore
those platforms advocate anonymity (which I agree with, in that use-case) which
does not enhance the socialising outcome desired by SCSP. SCSP encourages people
to use real names and a display picture to be explore-able and connect to people
using the platform (like twitter). In doing so, an user does not need to worry
about the platform moderating their free-speech or de-ranking their post. Posts
of an user are shown to anyone and everyone who chooses to follow them, and are
ranked fairly using their score and age as variables.

SCSP does not use any advertisement for its running. If it becomes unviable to 
keep it running, the hosting may be taken down but the project will always be
present on VCS. Anyone can clone/download this repository and host their own 
version (it will have its own point-of-truth). This is useful for organizations
or communities that wish to connect only within their group and not expose it to
the internet. The server's IP can be distributed to the employees and they can
use the platform as a totally disconnected network of its own. All functions 
still work.

### Build Details

Follow the following steps if building and deploying on linux. If you use 
windows / visual studio then figure it out yourself :)

To build the platform to be hosted on a server, one needs to have :

- dotnet-runtime
- nginx (or any reverse proxy)

Build the project to your runtime using 

```
dotnet -c Release -r linux-x64 --sc
```

replace linux-x64 to whatever server you are using.

after build, move the files into the server using rsync or scp. 

```
rsync -aP . user@servername:/var/www/scsp
```

Folder should be owned by user, not root.

```
chown sayan /var/www/scsp -R
chgrp users /var/www/scsp -R
```

In your server create a systemd service for the kestrel server to keep running:

```
[Unit]
Description=Run .NET Web App running on Ubuntu

[Service]
WorkingDirectory=/var/www/scsp
ExecStart=/usr/local/bin/dotnet /var/www/scsp/scsp.dll
Restart=always
# Restart service after 10 seconds if the dotnet service crashes:
RestartSec=10
KillSignal=SIGINT
SyslogIdentifier=dotnet-scsp
User=sayan

[Install]
WantedBy=multi-user.target
```

replace the user with your user.

Create a symlink in the project root which points to the dll to run:

```
ln bin/Release/net6.0/linux-x64/scsp.dll scsp.dll -s
```

Install nginx on server and configure it properly. 
An examples sites-available/default is provided: 

```
server {
    listen        80;
    server_name   sayn.work scsp.sayn.work;
    location / {
        proxy_pass         http://127.0.0.1:5000;
        proxy_http_version 1.1;
        proxy_set_header   Upgrade $http_upgrade;
        proxy_set_header   Connection keep-alive;
        proxy_set_header   Host $host;
        proxy_cache_bypass $http_upgrade;
        proxy_set_header   X-Forwarded-For $proxy_add_x_forwarded_for;
        proxy_set_header   X-Forwarded-Proto $scheme;
        proxy_buffer_size  4m;
        proxy_buffers       8 4m;
        proxy_busy_buffers_size 4m;
    }
}
```

The proxy-set-header are required for proper passing of request/response between
server and reverse-proxy. Also set the /etc/nginx/nginx.conf file so that 
POST requests can have large bodies. Otherwise image upload will fail. Example
is provided:

```
large_client_header_buffers 4 4m;
client_max_body_size    100m;
proxy_buffer_size  4m;
proxy_buffers       8 4m;
proxy_busy_buffers_size 4m;
```

add these lines inside the http{ body.

Start the server and reverse-proxy, platform should be running.

If any difficulties, follow the manuals:

- [Host ASP.NET Core on Linux with Nginx | Microsoft Learn](https://learn.microsoft.com/en-us/aspnet/core/host-and-deploy/linux-nginx?view=aspnetcore-7.0)
- [Nginx file upload error - too big file](https://stackoverflow.com/questions/45694544/nginx-file-upload-error-too-big-file)
- [dotnet build command](https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-build)

### Development

To develop (and hopefully contribute to this repository) you need to have

- dotnet-runtime
- dotnet-sdk
- aspnet-runtime

maybe also the targetting-packs.

Open the code on any editor and run `dotnet watch` on terminal, dotnet will
build and open project on chrome with hot-reload. 

In case of un-understandable errors like token error while hot-reloading, kill
the server (ctrl+c) and restart it.

### Contributions

If you find any bugs, unwanted behaviour, possible features / flairs to be added
kindly create an issue with appropiate comments. If possible also clone this
repository and contribute to fulfil that issue and create a pull request.


### License 

The project is licensed as per the LICENSE file


### Contributors

- Sayan Ghosh
- Aasif Khan
