using System;
using System.Linq;
using System.Collections.Generic;
using EzFtp.FileAPI;
using System.IO;

namespace EzFtp.Droid.Implementations
{
  public class StorageService : IStorageService
  {
    public const string StorageRoot = "/storage/";
    public const string DiskImage = "disk.png";
    public const string FolderImage = "folder.png";
    public const string FileImage = "file.png";

    public Phone Root { get; private set; }

    public StorageService()
    {
      SetRoot();      
    }

    public DiskItem[] GetChildren(DiskItem diskItem)
    {
      if(diskItem == null)
      {
        throw new ArgumentNullException(nameof(diskItem));
      }

      if(diskItem is RelativeFile)
      {
        throw new InvalidOperationException();
      }      

      if(ReferenceEquals(diskItem, Root) && Root.HasExtCard)
      {
        return GetSdCards();
      }
      
      var dirs = Directory.GetDirectories(diskItem.AbsolutePath);
      var files = Directory.GetFiles(diskItem.AbsolutePath);
      var rds = CreateRelativeDirectories(dirs);
      var rfs = CreateRelativeFiles(files);
      var items = new List<DiskItem>(rds.Length + rfs.Length);

      items.AddRange(rds);
      items.AddRange(rfs);
      return items.ToArray();
    }

    private RelativeFile[] CreateRelativeFiles(string[] files)
    {
      return files.Select(f => CreateRelativeFile(f)).ToArray();
    }

    private RelativeDirectory[] CreateRelativeDirectories(string[] dirs)
    {
      return dirs.Select(d => CreateRelativeDirectory(d)).ToArray();
    }

    private RelativeFile CreateRelativeFile(string file)
    {
      return new RelativeFile
      {
        Icon = FileImage,
        RelativePath = file.Substring(Root.RootPath.Length),
        RootPath = Root.RootPath,
        Name = Path.GetFileName(file)
      };
    }

    private RelativeDirectory CreateRelativeDirectory(string dir)
    {
      return new RelativeDirectory
      {
        Icon = FolderImage,
        RelativePath = dir.Substring(Root.RootPath.Length),
        RootPath = Root.RootPath,
        Name = Path.GetFileName(dir)
      };
    }

    private void SetRoot()
    {
      var dirs = Directory.GetDirectories(StorageRoot)
        .Where(d => d.IndexOf("sdcard", StringComparison.OrdinalIgnoreCase) >= 1)
        .ToList();

      Root = new Phone
      {
        Icon = string.Empty,
        LocalizedName = NetResource.MyPhone,
        RelativePath = string.Empty,
        Name = string.Empty
      };
      
      if (dirs.Count == 1)
      {
        Root.RootPath = StorageRoot + Path.GetFileName(dirs[0]) + "/";
        Root.HasExtCard = false;
      }
      else
      {
        Root.RootPath = StorageRoot;
        Root.HasExtCard = true;
      }
    }

    private DiskItem[] GetSdCards()
    {
      var dirs = Directory.GetDirectories(Root.RootPath)
        .Select(d => Path.GetFileName(d))
        .ToList();
      var rds = new List<DiskItem>();
      var dir = dirs.FirstOrDefault(d => d.StartsWith("sdcard", StringComparison.OrdinalIgnoreCase));
      var name = Path.GetFileName(dir);
      var disk = new Disk
      {
        Icon = DiskImage,
        LocalizedName = NetResource.PhoneStroage,
        RelativePath = name,
        RootPath = Root.RootPath,
        Name = name
      };
      rds.Add(disk);

      dir = dirs.FirstOrDefault(d => d.StartsWith("extsdcard", StringComparison.OrdinalIgnoreCase));
      name = Path.GetFileName(dir);
      disk = new Disk
      {
        Icon = DiskImage,
        LocalizedName = NetResource.SDCardStroage,
        RelativePath = name,
        RootPath = Root.RootPath,
        Name = name
      };

      rds.Add(disk);
      return rds.ToArray();
    }
  }
}