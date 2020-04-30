using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Queries
{
    public static class EasyBuyQueries
    {
         public const string CART_DETAILS = @"select sch_Cart_no   as cartId,
                                              sch_cart_name as cartName,
                                              sch_crt_id    as cartCreatedBy,
                                              sc.sch_crt_dt as cartCreatedOn,
                                              sch_tot_Val   as cartValue,
                                              sch_status    as cartStatusId,
                                              STATUS_DESC   as cartStatusDescription,
                                              sch_dept      as departmentId,
                                              deptdesc      as departmentDescription
                                         from sapsur.t_shopping_cart_hdr sc
                                         join sapsur.t_user_master um
                                           on sc.sch_crt_id = um.um_usr_id
                                         join sapsur.t_status_master sm
                                           on sc.sch_status = sm.status_cd
                                          and sm.status_of = 'SC_H'
                                         join sapsur.t_s_Dept_Mst dm
                                           on sc.Sch_Dept = dm.deptno
                                          and dm.client = '600'
                                        where sc.Sch_Cart_No not like '2%'
                                          and sc.Sch_Status not in ('01', '02')
                                          and sc.sch_crt_dt > sysdate -1"
;
    }
}
