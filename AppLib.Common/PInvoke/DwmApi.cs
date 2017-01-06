using System;
using System.Runtime.InteropServices;

namespace AppLib.Common.PInvoke
{
    /// <summary>
    /// Platform Invokes to Dwmapi.dll
    /// </summary>
    public static class DwmApi
    {
        /// <summary>
        /// Gets the current theme colors
        /// </summary>
        /// <param name="pars">Colors in a DWMCOLORIZATIONPARAMS structure</param>
        [DllImport("dwmapi.dll", EntryPoint = "#127")]
        public static extern void DwmGetColorizationParameters(ref DWMCOLORIZATIONPARAMS pars);

        /// <summary>
        /// Creates a Desktop Window Manager (DWM) thumbnail relationship between the destination and source windows.
        /// </summary>
        /// <param name="dest">The handle to the window that will use the DWM thumbnail. Setting the destination window handle to anything other than a top-level window type will result in a return value of E_INVALIDARG.</param>
        /// <param name="src">The handle to the window to use as the thumbnail source. Setting the source window handle to anything other than a top-level window type will result in a return value of E_INVALIDARG.</param>
        /// <param name="thumb">A pointer to a handle that, when this function returns successfully, represents the registration of the DWM thumbnail.</param>
        /// <returns></returns>
        [DllImport("dwmapi.dll")]
        public  static extern int DwmRegisterThumbnail(IntPtr dest, IntPtr src, out IntPtr thumb);

        /// <summary>
        /// The handle to the thumbnail relationship to be removed. Null or non-existent handles will result in a return value of E_INVALIDARG.
        /// </summary>
        /// <param name="thumb"></param>
        /// <returns>If this function succeeds, it returns S_OK. Otherwise, it returns an HRESULT error code.</returns>
        [DllImport("dwmapi.dll")]
        public static extern int DwmUnregisterThumbnail(IntPtr thumb);

        /// <summary>
        /// Retrieves the source size of the Desktop Window Manager (DWM) thumbnail.
        /// </summary>
        /// <param name="thumb">A handle to the thumbnail to retrieve the source window size from.</param>
        /// <param name="size">A pointer to a SIZE structure that, when this function returns successfully, receives the size of the source thumbnail.</param>
        /// <returns>If this function succeeds, it returns S_OK. Otherwise, it returns an HRESULT error code.</returns>
        [DllImport("dwmapi.dll")]
        public static extern int DwmQueryThumbnailSourceSize(IntPtr thumb, out SIZE size);


        /// <summary>
        /// Updates the properties for a Desktop Window Manager (DWM) thumbnail.
        /// </summary>
        /// <param name="hThumb">The handle to the DWM thumbnail to be updated. Null or invalid thumbnails, as well as thumbnails owned by other processes will result in a return value of E_INVALIDARG.</param>
        /// <param name="props">A pointer to a DWM_THUMBNAIL_PROPERTIES structure that contains the new thumbnail properties.</param>
        /// <returns>If this function succeeds, it returns S_OK. Otherwise, it returns an HRESULT error code.</returns>
        [DllImport("dwmapi.dll")]
        public static extern int DwmUpdateThumbnailProperties(IntPtr hThumb, ref DWM_THUMBNAIL_PROPERTIES props);

        /// <summary>
        /// Extends the window frame into the client area.
        /// </summary>
        /// <param name="hwnd">The handle to the window in which the frame will be extended into the client area.</param>
        /// <param name="pMarInset">A pointer to a MARGINS structure that describes the margins to use when extending the frame into the client area.</param>
        /// <returns>If this function succeeds, it returns S_OK. Otherwise, it returns an HRESULT error code.</returns>
        [DllImport("DwmApi.dll")]
        public static extern int DwmExtendFrameIntoClientArea(IntPtr hwnd, ref MARGINS pMarInset);

        /// <summary>
        /// Obtains a value that indicates whether Desktop Window Manager (DWM) composition is enabled. Applications on machines running Windows 7 or earlier can listen for composition state changes by handling the WM_DWMCOMPOSITIONCHANGED notification.
        /// </summary>
        /// <remarks>As of Windows 8, DWM composition is always enabled. If an app declares Windows 8 compatibility in their manifest, this function will receive a value of TRUE through pfEnabled. If no such manifest entry is found, Windows 8 compatibility is not assumed and this function receives a value of FALSE through pfEnabled. This is done so that older programs that interpret a value of TRUE to imply that high contrast mode is off can continue to make the correct decisions about rendering their images. (Note that this is a bad practice—you should use the SystemParametersInfo function with the SPI_GETHIGHCONTRAST flag to determine the state of high contrast mode.)</remarks>
        /// <param name="enabled">A pointer to a value that, when this function returns successfully, receives TRUE if DWM composition is enabled; otherwise, FALSE. </param>
        /// <returns>If this function succeeds, it returns S_OK. Otherwise, it returns an HRESULT error code.</returns>
        [DllImport("dwmapi.dll")]
        public static extern int DwmIsCompositionEnabled(out bool enabled);
    }
}
