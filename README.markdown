# What is bgrCopySharePath

This is a Windows utility that allows you to quickly get the links to files
from shared folders and paste them to another person on same local network.

It was written in 2006, for the specific usage scenario - sharing links on
[BorgChat](http://borgchat.10n.ro/), a peer-to-peer LAN-only chat app that our
group of friends from the neighborhood used extensively at the time. Although
the utility still works fine, it outputs the links formatted specially for
BorgChat.  It's been a long time since we've moved on from BorgChat to various
internet-based chats, but over the past few years there have been a few
occasions where I missed this app's functionality, so I've decided to dig it
out and revamp it (which is still TODO).


# Original release

I've included a "release" from that era, frozen in time. To see the utility in
all its 2007 glory you can open `uputstvo.exe`, an interactive video manual
that "shipped" with it, which demonstrates the installation and usage. It's in
Serbian but worth checking out for the "blast from the past" effect. Since the
only intended users were my friends and me, the app is also in Serbian.


## What is bgrCopySharePathSecret

As I anticipated the nightmare of nagging each of my non-techie friends to go
through the whole installation process every time I update the app, it also had
a built-in auto-updater functionality. The way it worked is that on my computer
there was a hidden shared folder with the latest version which the app queried
whenever anyone ran it. It also kept a log of who updated, a version of which
I've included in the release, for the sake of keeping the "frozen in time"
spirit intact (although it seems incomplete, I must've trimmed the file
manually back then since there were many more users of the app).


# Licenses

The utility relies on 3rd party code (Shares.cs), released by Richard Deeming
under [The Code Project Open License (CPOL)](https://www.codeproject.com/info/cpol10.aspx),
and originates at [this article](https://www.codeproject.com/Articles/2939/Network-Shares-and-UNC-paths).

As for the bgrCopySharePath itself, it states that it's "Copyright © buger 2007".
I'll see what I can do about that.
