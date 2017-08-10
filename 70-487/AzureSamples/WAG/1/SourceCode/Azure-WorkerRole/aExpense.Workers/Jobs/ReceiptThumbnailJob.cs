//===============================================================================
// Microsoft patterns & practices
// Windows Azure Architecture Guide
//===============================================================================
// Copyright © Microsoft Corporation.  All rights reserved.
// This code released under the terms of the 
// Microsoft patterns & practices license (http://wag.codeplex.com/license)
//===============================================================================


namespace AExpense.Workers.Jobs
{
    using System;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Linq;
    using AExpense.DataAccessLayer;
    using AExpense.Jobs;
    using AExpense.QueueMessages;
    using AExpense.Queues;

    public class ReceiptThumbnailJob : BaseJobProcessor<NewReceiptMessage>
    {
        private static readonly int ThumbnailSize = 65;
        private static readonly int PhotoSize = 330;
        private ExpenseReceiptStorage receiptStorage;
        private ExpenseRepository expenseRepository;
        
        public ReceiptThumbnailJob() : base(2000, new AzureQueueContext())
        {
            this.receiptStorage = new ExpenseReceiptStorage();
            this.expenseRepository = new ExpenseRepository();
        }

        public override bool ProcessMessage(NewReceiptMessage message)
        {
            var expenseItemId = message.ExpenseItemId;
            var imageName = expenseItemId + ".jpg";

            byte[] originalReceipt = this.receiptStorage.GetReceipt(expenseItemId);

            if (originalReceipt != null && originalReceipt.Length > 0)
            {
                var thumb = ResizeImage(originalReceipt, ThumbnailSize);
                var thumbUri = this.receiptStorage.AddReceipt(Path.Combine("thumbnails", imageName), thumb, "image/jpeg");

                var receipt = ResizeImage(originalReceipt, PhotoSize);
                var receiptUri = this.receiptStorage.AddReceipt(imageName, receipt, "image/jpeg");

                this.expenseRepository.UpdateExpenseItemImages(expenseItemId, receiptUri, thumbUri);

                this.receiptStorage.DeleteReceipt(expenseItemId);

                return true;
            }

            return false;
        }

        private static byte[] ResizeImage(byte[] imageData, int size)
        {
            var image = Image.FromStream(new MemoryStream(imageData));
            int newHeight;
            int newWidth;
            if (image.Width > image.Height)
            {
                newHeight = (int)Math.Round(image.Height * ((1 / (decimal)image.Width) * size));
                newWidth = size;
            }
            else
            {
                newWidth = (int)Math.Round(image.Width * ((1 / (decimal)image.Height) * size));
                newHeight = size;
            }

            var thumb = new Bitmap(size, size);
            using (var g = Graphics.FromImage(thumb))
            {
                int startX = (size - newWidth) / 2;
                int startY = (size - newHeight) / 2;
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
                g.Clear(Color.White);
                g.DrawImage(image, startX, startY, newWidth, newHeight);
            }

            byte[] newImage;
            using (var stream = new MemoryStream())
            {
                thumb.Save(stream, GetImageCodec(), GetImageCodecParameters());
                newImage = stream.ToArray();
            }

            return newImage;
        }

        private static ImageCodecInfo GetImageCodec()
        {
            return ImageCodecInfo.GetImageEncoders()
                                 .Where(c => c.FormatID == ImageFormat.Jpeg.Guid)
                                 .Single();
        }

        private static EncoderParameters GetImageCodecParameters()
        {
            var parameters = new EncoderParameters(1);
            parameters.Param[0] = new EncoderParameter(Encoder.Quality, 80L);
            return parameters;
        }
    }
}
